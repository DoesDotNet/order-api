using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using Shop.Orders.Api.Models;
using Newtonsoft.Json;
using Shop.Orders.Application.Commands;
using System.Threading;

namespace Shop.Orders.Api
{
    public class CreateOrderFunction
    {
        private readonly ILogger<CreateOrderFunction> _logger;
        private readonly IMediator _mediator;

        public CreateOrderFunction(ILogger<CreateOrderFunction> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [FunctionName("CreateOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "")] HttpRequest req,
            CancellationToken cancellationToken
            )
        {
            _logger.LogInformation("Creating Order");

            string json = await req.ReadAsStringAsync();
            var createOrderModel = JsonConvert.DeserializeObject<CreateOrderModel>(json);

            var command = new CreateOrder(createOrderModel.Id, createOrderModel.CustomerId);

            try
            {
                await _mediator.Send(command, cancellationToken);
                return new StatusCodeResult(201);
            }
            catch (DomainValidationException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
