using CaixaModernApi.Domain;
using CaixaModernApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaixaModernApi.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext dbContext;

        public TodoRepository(AppDbContext dbContext) => this.dbContext = dbContext;

        public async Task<IEnumerable<Todo>> GetAll()
            => await dbContext.Todos.ToListAsync();

        public async Task<Todo?> GetById(int id)
            => await dbContext.Todos.FindAsync(id);
    }
}
