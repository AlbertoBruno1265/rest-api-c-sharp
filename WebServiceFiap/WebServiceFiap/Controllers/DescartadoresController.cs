using Microsoft.AspNetCore.Mvc;
using WebServiceFiap.Models;
using WebServiceFiap.Services;

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
            var descartadores = _service.GetPaged(safePage, safePageSize);

            return Ok(new
            {
                page = safePage,
                pageSize = safePageSize,
                totalItems = _service.Count(),
                items = descartadores
            });
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var descartador = _service.GetById(id);

            if (descartador == null)
                return NotFound($"Descartador com ID {id} nao encontrado.");

            return Ok(descartador);
        }

        [HttpPost]
        public IActionResult Create([FromBody] DescartadorModel novoDescartador)
        {
            _service.Add(novoDescartador);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoDescartador.Id },
                novoDescartador
            );
        }

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, [FromBody] DescartadorModel descartador)
        {
            var descartadorExistente = _service.GetById(id);

            if (descartadorExistente == null)
                return NotFound($"Descartador com ID {id} nao encontrado.");

            descartador.Id = id;

            _service.Update(descartador);

            return NoContent();
        }

        [HttpDelete("{id:long}")]
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
