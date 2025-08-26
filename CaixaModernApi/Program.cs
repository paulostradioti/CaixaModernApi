using CaixaModernApi.Data;
using CaixaModernApi.Data.Repositories;
using CaixaModernApi.Domain.Interfaces;
using CaixaModernApi.Filters;
using CaixaModernApi.Security.IpAllowlist;
using CaixaModernApi.UseCases.Queries;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CaixaModernApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DefaultCors", policy =>
                {
                    var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
                    if (origins is { Length: > 0 })
                    {
                        policy.WithOrigins(origins)
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                        // If you need cookies/credentials: add .AllowCredentials() and ensure origins are not "*"
                    }
                    else
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    }
                });
            });

            builder.Services.AddControllers(opts => opts.Filters.Add<CustomExceptionsFilter>());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCortexMediator(builder.Configuration, [typeof(GetByIdQuery)], opt => opt.AddDefaultBehaviors());

            builder.Services.Configure<IpAllowlistOptions>(builder.Configuration.GetSection("IpAllowlist"));
            builder.Services.AddScoped<IpAllowlistAuthorizationFilter>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("DataSource=Caixa.db"));
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Use CORS before authorization/endpoints
            app.UseCors("DefaultCors");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
