using Microsoft.AspNetCore.Mvc;

using TodoApp.Models;

using TodoApp.Services;


namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly TodoService _service;

        public TodosController(TodoService service)
        {
            _service = service;
        }

        /// <summary>
        /// GET: api/todos
        /// Retrieves all to-do items.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        /// <summary>
        /// GET: api/todos/{id}
        /// Retrieves a single to-do item by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// POST: api/todos
        /// Creates a new to-do item.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Create(TodoItem item)
        {
            var created = await _service.CreateAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// PUT: api/todos/{id}
        /// Updates an existing to-do item.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TodoItem item)
        {
            if (id != item.Id) return BadRequest();

            var updated = await _service.UpdateAsync(item);
            if (updated) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// DELETE: api/todos/{id}
        /// Deletes a to-do item.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
