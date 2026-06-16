using Microsoft.AspNetCore.Mvc;
using WebServiceFiap.Models;
using WebServiceFiap.Services;

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
            var centros = _service.GetPaged(safePage, safePageSize);

            return Ok(new
            {
                page = safePage,
                pageSize = safePageSize,
                totalItems = _service.Count(),
                items = centros
            });
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var centro = _service.GetById(id);

            if (centro == null)
                return NotFound($"Centro de coleta com ID {id} nao encontrado.");

            return Ok(centro);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CentroColetaModel novoCentro)
        {
            _service.Add(novoCentro);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoCentro.Id },
                novoCentro
            );
        }

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, [FromBody] CentroColetaModel centro)
        {
            var centroExistente = _service.GetById(id);

            if (centroExistente == null)
                return NotFound($"Centro de coleta com ID {id} nao encontrado.");

            centro.Id = id;

            _service.Update(centro);

            return NoContent();
        }

        [HttpDelete("{id:long}")]
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
