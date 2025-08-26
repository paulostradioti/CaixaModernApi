using CaixaModernApi.Domain;
using CaixaModernApi.Domain.Exceptions;
using CaixaModernApi.Domain.Interfaces;
using Cortex.Mediator.Queries;

namespace CaixaModernApi.UseCases.Queries
{
    public record GetByIdQuery(int Id) : IQuery<Todo>;
    public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, Todo>
    {
        private readonly ITodoRepository repository;
        public GetByIdQueryHandler(ITodoRepository repository) => this.repository = repository;

        public async Task<Todo> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        {
            var todo = await repository.GetById(query.Id);
            if (todo == null)
                throw new EntityNotFoundException(typeof(Todo), query.Id);

            return todo;
        }
    }
}
