using CaixaModernApi.Domain;
using CaixaModernApi.Security.IpAllowlist;
using CaixaModernApi.UseCases.Queries;
using Cortex.Mediator;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CaixaModernApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [IpAllowlist]
    //[ServiceFilter(typeof(IpAllowlistAuthorizationFilter))]
    public class TodoController : ControllerBase
    {
        private IMediator mediator;
        public TodoController(IMediator mediator) => this.mediator = mediator;

        [HttpGet]
        public async Task<Todo> GetById(int id)
        {
            var query = new GetByIdQuery(id);
            var ret = await mediator.SendQueryAsync<GetByIdQuery, Todo>(query);

            return ret;
        }
    }
}
