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
    public class CatadoresController : ControllerBase
    {
        private readonly CatadorService _service;
 
        public CatadoresController(CatadorService service)
        {
            _service = service;
        }
 
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var safePage = Math.Max(page, 1);
            var safePageSize = Math.Clamp(pageSize, 1, 100);
 
            var catadores = _service.GetPaged(safePage, safePageSize)
                .Select(c => new CatadorResponse
                {
                    Id = c.Id,
                    CapacidadeVolumeTotal = c.CapacidadeVolumeTotal
                });
 
            return Ok(new PagedResponse<CatadorResponse>(catadores, safePage, safePageSize, _service.Count()));
        }
 
        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var catador = _service.GetById(id);
 
            if (catador == null)
                return NotFound($"Catador com ID {id} nao encontrado.");
 
            return Ok(new CatadorResponse
            {
                Id = catador.Id,
                CapacidadeVolumeTotal = catador.CapacidadeVolumeTotal
            });
        }
 
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CatadorRequest request)
        {
            var novoCatador = new CatadorModel
            {
                CapacidadeVolumeTotal = request.CapacidadeVolumeTotal
            };
 
            _service.Add(novoCatador);
 
            return CreatedAtAction(
                nameof(GetById),
                new { id = novoCatador.Id },
                new CatadorResponse { Id = novoCatador.Id, CapacidadeVolumeTotal = novoCatador.CapacidadeVolumeTotal }
            );
        }
 
        [HttpPut("{id:long}")]
        [Authorize]
        public IActionResult Update(long id, [FromBody] CatadorRequest request)
        {
            var catadorExistente = _service.GetById(id);
 
            if (catadorExistente == null)
                return NotFound($"Catador com ID {id} nao encontrado.");
 
            catadorExistente.CapacidadeVolumeTotal = request.CapacidadeVolumeTotal;
 
            _service.Update(catadorExistente);
 
            return NoContent();
        }
 
        [HttpDelete("{id:long}")]
        [Authorize]
        public IActionResult Delete(long id)
        {
            var catador = _service.GetById(id);
 
            if (catador == null)
                return NotFound($"Catador com ID {id} nao encontrado.");
 
            _service.Delete(id);
 
            return NoContent();
        }
    }
}