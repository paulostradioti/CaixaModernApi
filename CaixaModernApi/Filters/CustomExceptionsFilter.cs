using CaixaModernApi.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CaixaModernApi.Filters
{
    public class CustomExceptionsFilter : IActionFilter
    {
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        public CustomExceptionsFilter(ProblemDetailsFactory problemDetailsFactory)
            => _problemDetailsFactory = problemDetailsFactory;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is EntityNotFoundException entityNotFoundException)
            {

                var httpContext = context.HttpContext;

                var problem = _problemDetailsFactory.CreateProblemDetails(
                    httpContext,
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Entity not found",
                    type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                    detail: entityNotFoundException.Message
                );

                // Enrich ProblemDetails with context
                if (!string.IsNullOrWhiteSpace(entityNotFoundException.EntityName))
                    problem.Extensions["entity"] = entityNotFoundException.EntityName;

                if (entityNotFoundException.Key is not null)
                    problem.Extensions["key"] = entityNotFoundException.Key;

                problem.Extensions["traceId"] = httpContext.TraceIdentifier;

                context.Result = new ObjectResult(problem) { StatusCode = problem.Status };
                context.ExceptionHandled = true;
            }
        }
    }
}