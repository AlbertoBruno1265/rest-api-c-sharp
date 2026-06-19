using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebServiceFiap.Models;
using WebServiceFiap.Services;
using WebServiceFiap.ViewModels.Request;
using WebServiceFiap.ViewModels.Response;
 
namespace WebServiceFiap.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
 
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }
 
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var safePage = Math.Max(page, 1);
            var safePageSize = Math.Clamp(pageSize, 1, 100);
 
            var usuarios = _service.GetPaged(safePage, safePageSize)
                .Select(u => new UsuarioResponse
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Funcao = u.Funcao
                    // Senha NUNCA é exposta
                });
 
            return Ok(new PagedResponse<UsuarioResponse>(usuarios, safePage, safePageSize, _service.Count()));
        }
 
        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var usuario = _service.GetById(id);
 
            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");
 
            return Ok(new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Funcao = usuario.Funcao
            });
        }
 
        [HttpPost]
        public IActionResult Create([FromBody] UsuarioRequest request)
        {
            var novoUsuario = new UsuarioModel
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha, // Hash aplicado no Service
                Funcao = request.Funcao
            };
 
            _service.Add(novoUsuario);
 
            return CreatedAtAction(
                nameof(GetById),
                new { id = novoUsuario.Id },
                new UsuarioResponse
                {
                    Id = novoUsuario.Id,
                    Nome = novoUsuario.Nome,
                    Email = novoUsuario.Email,
                    Funcao = novoUsuario.Funcao
                }
            );
        }
 
        [HttpPut("{id:long}")]
        [Authorize]
        public IActionResult Update(long id, [FromBody] UsuarioUpdateRequest request)
        {
            var usuarioExistente = _service.GetById(id);
 
            if (usuarioExistente == null)
                return NotFound($"Usuário com ID {id} não encontrado.");
 
            usuarioExistente.Nome = request.Nome;
            usuarioExistente.Email = request.Email;
            usuarioExistente.Funcao = request.Funcao;
            // Senha não é alterada neste endpoint
 
            _service.Update(usuarioExistente);
 
            return NoContent();
        }
 
        [HttpDelete("{id:long}")]
        [Authorize]
        public IActionResult Delete(long id)
        {
            var usuario = _service.GetById(id);
 
            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");
 
            _service.Delete(id);
 
            return NoContent();
        }
    }
}