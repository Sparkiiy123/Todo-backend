using Microsoft.EntityFrameworkCore;

namespace Todobackend.Services
{
    public class TodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            _context.Tasks.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // Pilna UpdateAsync ar TodoItem
        public async Task<TodoItem?> UpdateAsync(TodoItem item)
        {
            var existing = await _context.Tasks.FindAsync(item.Id);
            if (existing == null) return null;

            existing.Title = item.Title;
            existing.IsDone = item.IsDone;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Tasks.FindAsync(id);
            if (item == null) return false;

            _context.Tasks.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
