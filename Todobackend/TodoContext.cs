using Microsoft.EntityFrameworkCore;

namespace Todobackend
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<TodoItem> Tasks { get; set; }
    }
}
