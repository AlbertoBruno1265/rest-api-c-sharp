using Microsoft.AspNetCore.Mvc;
using WebServiceFiap.Data.Contexts;
using WebServiceFiap.Models;
using WebServiceFiap.Repository;
using WebServiceFiap.Repository.AbstractRepo;
using WebServiceFiap.Services;

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
        public IActionResult GetAll()
        {
            var usuarios = _service.GetAll();

            return Ok(usuarios);
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var usuario = _service.GetById(id);

            if (usuario == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UsuarioModel novoUsuario)
        {
            _service.Add(novoUsuario);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoUsuario.Id },
                novoUsuario
            );
        }

        [HttpPut("{id:long}")]
        public IActionResult Update(
            long id,
            [FromBody] UsuarioModel usuario)
        {
            var usuarioExistente = _service.GetById(id);

            if (usuarioExistente == null)
                return NotFound($"Usuário com ID {id} não encontrado.");

            usuario.Id = id;

            _service.Update(usuario);

            return NoContent();
        }

        [HttpDelete("{id:long}")]
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
