using Microsoft.AspNetCore.Mvc;
using WebServiceFiap.Models;
using WebServiceFiap.Services;

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
            var coletas = _service.GetPaged(safePage, safePageSize);

            return Ok(new
            {
                page = safePage,
                pageSize = safePageSize,
                totalItems = _service.Count(),
                items = coletas
            });
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var coleta = _service.GetById(id);

            if (coleta == null)
                return NotFound($"Coleta com ID {id} nao encontrada.");

            return Ok(coleta);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ColetaModel novaColeta)
        {
            _service.Add(novaColeta);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novaColeta.IdColeta },
                novaColeta
            );
        }

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, [FromBody] ColetaModel coleta)
        {
            var coletaExistente = _service.GetById(id);

            if (coletaExistente == null)
                return NotFound($"Coleta com ID {id} nao encontrada.");

            coleta.IdColeta = id;

            _service.Update(coleta);

            return NoContent();
        }

        [HttpDelete("{id:long}")]
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
