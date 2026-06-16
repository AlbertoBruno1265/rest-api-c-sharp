using Microsoft.AspNetCore.Mvc;
using WebServiceFiap.Models;
using WebServiceFiap.Services;

namespace WebServiceFiap.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItensController : ControllerBase
    {
        private readonly ItemService _service;

        public ItensController(ItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var safePage = Math.Max(page, 1);
            var safePageSize = Math.Clamp(pageSize, 1, 100);
            var itens = _service.GetPaged(safePage, safePageSize);

            return Ok(new
            {
                page = safePage,
                pageSize = safePageSize,
                totalItems = _service.Count(),
                items = itens
            });
        }

        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var item = _service.GetById(id);

            if (item == null)
                return NotFound($"Item com ID {id} nao encontrado.");

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ItemModel novoItem)
        {
            _service.Add(novoItem);

            return CreatedAtAction(
                nameof(GetById),
                new { id = novoItem.Id },
                novoItem
            );
        }

        [HttpPut("{id:long}")]
        public IActionResult Update(long id, [FromBody] ItemModel item)
        {
            var itemExistente = _service.GetById(id);

            if (itemExistente == null)
                return NotFound($"Item com ID {id} nao encontrado.");

            item.Id = id;

            _service.Update(item);

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            var item = _service.GetById(id);

            if (item == null)
                return NotFound($"Item com ID {id} nao encontrado.");

            _service.Delete(id);

            return NoContent();
        }
    }
}
