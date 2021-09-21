using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using MediatR;
using Shop.Orders.Application.Queries;
using System.Threading;

namespace Shop.Orders.Api.Functions
{
    public class GetOrderFunction
    {
        private readonly ILogger<CreateOrderFunction> _logger;
        private readonly IMediator _mediator;

        public GetOrderFunction(ILogger<CreateOrderFunction> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [FunctionName("GetOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{id}")] HttpRequest req, 
            Guid id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Order");

            var query = new GetOrder(id);

            var orderDataView = await _mediator.Send(query, cancellationToken);

            return new OkObjectResult(orderDataView);
        }
    }
}
