using Microsoft.AspNetCore.Mvc;
using Todobackend.Services;

namespace Todobackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoService _service;

        public TodoItemsController(TodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItem item)
        {
            var created = await _service.CreateAsync(item);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoItem item)
        {
            if (item.Id == 0)
            {
                item.Id = id;
            }


            if (id != item.Id)
            {
                return BadRequest("Id in URL does not match Id in body");
            }

            var updated = await _service.UpdateAsync(item);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
