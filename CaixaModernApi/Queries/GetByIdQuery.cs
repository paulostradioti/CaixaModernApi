using Cortex.Mediator.Queries;

namespace CaixaModernApi.Queries
{
    public record GetByIdQuery(Guid Id) : IQuery<Todo>;


    public class GetByIdQueryHandler2 : IQueryHandler<GetByIdQuery, Todo>
    {
        public async Task<Todo> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        {
            Console.WriteLine("Executou 2");
            return new Todo { Id = query.Id, Title = "Titulo Aleatorio 2" };
        }
    }

    public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, Todo>
    {
        public async Task<Todo> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        {
            Console.WriteLine("Executou 1");
            return new Todo { Id = query.Id, Title = "Titulo Aleatorio 1" };
        }
    }

}
