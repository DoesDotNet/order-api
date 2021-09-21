using FluentValidation;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shop.Orders.Application.Commands;
using Shop.Orders.Application.Data;
using Shop.Orders.Repository.InMemory;

[assembly: FunctionsStartup(typeof(Shop.Orders.Api.Startup))]

namespace Shop.Orders.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(typeof(CreateOrder));
            builder.Services.AddScoped<IRepository, InMemoryRepository>();


            builder.Services.AddScoped<IValidator<CreateOrder>, CreateOrderValidator>();
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        }
    }
}
