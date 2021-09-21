using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using Shop.Orders.Application.Commands;
using Shop.Orders.Api.Models;
using System.Threading;

namespace Shop.Orders.Api.Functions
{
    public class AddOrderLineFunction
    {
        private readonly ILogger<CreateOrderFunction> _logger;
        private readonly IMediator _mediator;

        public AddOrderLineFunction(ILogger<CreateOrderFunction> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [FunctionName("AddOrderLine")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "{id}/lines")] HttpRequest req, Guid id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Adding order line");

            string json = await req.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<AddOrderLineModel>(json);

            var command = model.ToCommand(id);

            await _mediator.Send(command, cancellationToken);

            return new StatusCodeResult(201);
        }
    }
}
