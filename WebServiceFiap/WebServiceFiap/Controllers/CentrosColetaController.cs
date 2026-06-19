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
    public class CentrosColetaController : ControllerBase
    {
        private readonly CentroColetaService _service;
 
        public CentrosColetaController(CentroColetaService service)
        {
            _service = service;
        }
 
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var safePage = Math.Max(page, 1);
            var safePageSize = Math.Clamp(pageSize, 1, 100);
 
            var centros = _service.GetPaged(safePage, safePageSize)
                .Select(c => new CentroColetaResponse
                {
                    Id = c.Id,
                    Endereco = c.Endereco,
                    VolumeItensTotal = c.VolumeItensTotal,
                    VolumeItensAtual = c.VolumeItensAtual
                });
 
            return Ok(new PagedResponse<CentroColetaResponse>(centros, safePage, safePageSize, _service.Count()));
        }
 
        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var centro = _service.GetById(id);
 
            if (centro == null)
                return NotFound($"Centro de coleta com ID {id} nao encontrado.");
 
            return Ok(new CentroColetaResponse
            {
                Id = centro.Id,
                Endereco = centro.Endereco,
                VolumeItensTotal = centro.VolumeItensTotal,
                VolumeItensAtual = centro.VolumeItensAtual
            });
        }
 
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CentroColetaRequest request)
        {
            var novoCentro = new CentroColetaModel
            {
                Endereco = request.Endereco,
                VolumeItensTotal = request.VolumeItensTotal,
                VolumeItensAtual = request.VolumeItensAtual
            };
 
            _service.Add(novoCentro);
 
            return CreatedAtAction(
                nameof(GetById),
                new { id = novoCentro.Id },
                new CentroColetaResponse
                {
                    Id = novoCentro.Id,
                    Endereco = novoCentro.Endereco,
                    VolumeItensTotal = novoCentro.VolumeItensTotal,
                    VolumeItensAtual = novoCentro.VolumeItensAtual
                }
            );
        }
 
        [HttpPut("{id:long}")]
        [Authorize]
        public IActionResult Update(long id, [FromBody] CentroColetaRequest request)
        {
            var centroExistente = _service.GetById(id);
 
            if (centroExistente == null)
                return NotFound($"Centro de coleta com ID {id} nao encontrado.");
 
            centroExistente.Endereco = request.Endereco;
            centroExistente.VolumeItensTotal = request.VolumeItensTotal;
            centroExistente.VolumeItensAtual = request.VolumeItensAtual;
 
            _service.Update(centroExistente);
 
            return NoContent();
        }
 
        [HttpDelete("{id:long}")]
        [Authorize]
        public IActionResult Delete(long id)
        {
            var centro = _service.GetById(id);
 
            if (centro == null)
                return NotFound($"Centro de coleta com ID {id} nao encontrado.");
 
            _service.Delete(id);
 
            return NoContent();
        }
    }
}