using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;


namespace TodoApp.Services
{
    /// <summary>
    /// Provides CRUD operations for TodoItem.
    /// </summary>
    public class TodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all to-do items.
        /// </summary>
        public async Task<List<TodoItem>> GetAllAsync() =>
            await _context.TodoItems.ToListAsync();

        /// <summary>
        /// Retrieves a to-do item by ID.
        /// </summary>
        public async Task<TodoItem?> GetByIdAsync(int id) =>
            await _context.TodoItems.FindAsync(id);

        /// <summary>
        /// Creates a new to-do item.
        /// </summary>
        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        /// <summary>
        /// Updates an existing to-do item.
        /// </summary>
        public async Task<bool> UpdateAsync(TodoItem item)
        {
            var exists = await _context.TodoItems.AnyAsync(t => t.Id == item.Id);
            if (!exists) return false;

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a to-do item.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return false;

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
