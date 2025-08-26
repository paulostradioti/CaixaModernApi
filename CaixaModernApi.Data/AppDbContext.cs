using CaixaModernApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace CaixaModernApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos => Set<Todo>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
