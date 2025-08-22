using CaixaModernApi.Queries;
using Cortex.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CaixaModernApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private IMediator mediator;
        public TodoController(IMediator mediator) => this.mediator = mediator;


        [HttpGet]
        public async Task<Todo> GetById(Guid id)
        {
            var query = new GetByIdQuery(id);

            var ret = await mediator.SendQueryAsync<GetByIdQuery, Todo>(query);

            return ret;
        }
    }
}
