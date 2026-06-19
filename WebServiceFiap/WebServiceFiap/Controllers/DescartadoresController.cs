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
    public class DescartadoresController : ControllerBase
    {
        private readonly DescartadorService _service;
 
        public DescartadoresController(DescartadorService service)
        {
            _service = service;
        }
 
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var safePage = Math.Max(page, 1);
            var safePageSize = Math.Clamp(pageSize, 1, 100);
 
            var descartadores = _service.GetPaged(safePage, safePageSize)
                .Select(d => new DescartadorResponse
                {
                    Id = d.Id,
                    Endereco = d.Endereco
                });
 
            return Ok(new PagedResponse<DescartadorResponse>(descartadores, safePage, safePageSize, _service.Count()));
        }
 
        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var descartador = _service.GetById(id);
 
            if (descartador == null)
                return NotFound($"Descartador com ID {id} nao encontrado.");
 
            return Ok(new DescartadorResponse
            {
                Id = descartador.Id,
                Endereco = descartador.Endereco
            });
        }
 
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] DescartadorRequest request)
        {
            var novoDescartador = new DescartadorModel
            {
                Endereco = request.Endereco
            };
 
            _service.Add(novoDescartador);
 
            return CreatedAtAction(
                nameof(GetById),
                new { id = novoDescartador.Id },
                new DescartadorResponse { Id = novoDescartador.Id, Endereco = novoDescartador.Endereco }
            );
        }
 
        [HttpPut("{id:long}")]
        [Authorize]
        public IActionResult Update(long id, [FromBody] DescartadorRequest request)
        {
            var descartadorExistente = _service.GetById(id);
 
            if (descartadorExistente == null)
                return NotFound($"Descartador com ID {id} nao encontrado.");
 
            descartadorExistente.Endereco = request.Endereco;
 
            _service.Update(descartadorExistente);
 
            return NoContent();
        }
 
        [HttpDelete("{id:long}")]
        [Authorize]
        public IActionResult Delete(long id)
        {
            var descartador = _service.GetById(id);
 
            if (descartador == null)
                return NotFound($"Descartador com ID {id} nao encontrado.");
 
            _service.Delete(id);
 
            return NoContent();
        }
    }
}