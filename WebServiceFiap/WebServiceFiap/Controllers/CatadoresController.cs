using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebServiceFiap.Models;
using WebServiceFiap.Services;

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
            var catadores = _service.GetPaged(safePage, safePageSize);

            return Ok(new
            {
                page = safePage,
                pageSize = safePageSize,
                totalItems = _service.Count(),
                items = catadores
            });
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var catador = _service.GetById(id);

            if (catador == null)
                return NotFound($"Catador com ID {id} nao encontrado.");

            return Ok(catador);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CatadorModel novoCatador)
        {
            _service.Add(novoCatador);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoCatador.Id },
                novoCatador
            );
        }

        [HttpPut("{id:long}")]
        [Authorize]
        public IActionResult Update(long id, [FromBody] CatadorModel catador)
        {
            var catadorExistente = _service.GetById(id);

            if (catadorExistente == null)
                return NotFound($"Catador com ID {id} nao encontrado.");

            catador.Id = id;

            _service.Update(catador);

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
