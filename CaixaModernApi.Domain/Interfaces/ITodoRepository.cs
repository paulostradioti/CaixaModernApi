namespace CaixaModernApi.Domain.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAll();
        Task<Todo?> GetById(int id);
    }
}
