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
 
            var itens = _service.GetPaged(safePage, safePageSize)
                .Select(i => new ItemResponse
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Volume = i.Volume
                });
 
            return Ok(new PagedResponse<ItemResponse>(itens, safePage, safePageSize, _service.Count()));
        }
 
        [HttpGet("{id:long}")]
        public IActionResult GetById(long id)
        {
            var item = _service.GetById(id);
 
            if (item == null)
                return NotFound($"Item com ID {id} nao encontrado.");
 
            return Ok(new ItemResponse
            {
                Id = item.Id,
                Nome = item.Nome,
                Volume = item.Volume
            });
        }
 
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] ItemRequest request)
        {
            var novoItem = new ItemModel
            {
                Nome = request.Nome,
                Volume = request.Volume
            };
 
            _service.Add(novoItem);
 
            return CreatedAtAction(
                nameof(GetById),
                new { id = novoItem.Id },
                new ItemResponse { Id = novoItem.Id, Nome = novoItem.Nome, Volume = novoItem.Volume }
            );
        }
 
        [HttpPut("{id:long}")]
        [Authorize]
        public IActionResult Update(long id, [FromBody] ItemRequest request)
        {
            var itemExistente = _service.GetById(id);
 
            if (itemExistente == null)
                return NotFound($"Item com ID {id} nao encontrado.");
 
            itemExistente.Nome = request.Nome;
            itemExistente.Volume = request.Volume;
 
            _service.Update(itemExistente);
 
            return NoContent();
        }
 
        [HttpDelete("{id:long}")]
        [Authorize]
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