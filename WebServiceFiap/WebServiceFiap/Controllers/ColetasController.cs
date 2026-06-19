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
    public class ColetasController : ControllerBase
    {
        private readonly ColetaService _service;
 
        public ColetasController(ColetaService service)
        {
            _service = service;
        }
 
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var safePage = Math.Max(page, 1);
            var safePageSize = Math.Clamp(pageSize, 1, 100);
 
            var coletas = _service.GetPaged(safePage, safePageSize)
                .Select(c => new ColetaResponse
                {
                    IdColeta = c.IdColeta,
                    Data = c.Data,
                    IdCatador = c.IdCatador,
                    IdDescartador = c.IdDescartador,
                    IdCentro = c.IdCentro,
                    FoiFinalizada = c.FoiFinalizada
                });
 
            return Ok(new PagedResponse<ColetaResponse>(coletas, safePage, safePageSize, _service.Count()));
        }
 
        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var coleta = _service.GetById(id);
 
            if (coleta == null)
                return NotFound($"Coleta com ID {id} nao encontrada.");
 
            return Ok(new ColetaResponse
            {
                IdColeta = coleta.IdColeta,
                Data = coleta.Data,
                IdCatador = coleta.IdCatador,
                IdDescartador = coleta.IdDescartador,
                IdCentro = coleta.IdCentro,
                FoiFinalizada = coleta.FoiFinalizada
            });
        }
 
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] ColetaRequest request)
        {
            var novaColeta = new ColetaModel
            {
                Data = request.Data,
                IdCatador = request.IdCatador,
                IdDescartador = request.IdDescartador,
                IdCentro = request.IdCentro,
                FoiFinalizada = request.FoiFinalizada
            };
 
            _service.Add(novaColeta);
 
            return CreatedAtAction(
                nameof(GetById),
                new { id = novaColeta.IdColeta },
                new ColetaResponse
                {
                    IdColeta = novaColeta.IdColeta,
                    Data = novaColeta.Data,
                    IdCatador = novaColeta.IdCatador,
                    IdDescartador = novaColeta.IdDescartador,
                    IdCentro = novaColeta.IdCentro,
                    FoiFinalizada = novaColeta.FoiFinalizada
                }
            );
        }
 
        [HttpPut("{id:long}")]
        [Authorize]
        public IActionResult Update(long id, [FromBody] ColetaRequest request)
        {
            var coletaExistente = _service.GetById(id);
 
            if (coletaExistente == null)
                return NotFound($"Coleta com ID {id} nao encontrada.");
 
            coletaExistente.Data = request.Data;
            coletaExistente.IdCatador = request.IdCatador;
            coletaExistente.IdDescartador = request.IdDescartador;
            coletaExistente.IdCentro = request.IdCentro;
            coletaExistente.FoiFinalizada = request.FoiFinalizada;
 
            _service.Update(coletaExistente);
 
            return NoContent();
        }
 
        [HttpDelete("{id:long}")]
        [Authorize]
        public IActionResult Delete(long id)
        {
            var coleta = _service.GetById(id);
 
            if (coleta == null)
                return NotFound($"Coleta com ID {id} nao encontrada.");
 
            _service.Delete(id);
 
            return NoContent();
        }
    }
}