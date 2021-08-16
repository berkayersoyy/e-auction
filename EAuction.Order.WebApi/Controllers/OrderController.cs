
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EAuction.Order.Application.Commands.OrderCreate;
using EAuction.Order.Application.Queries;
using EAuction.Order.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EAuction.Order.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("GetOrdersByUserName/{userName}")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersBySellerUserNameQuery(userName);

            var orders = await _mediator.Send(query);
            if (orders.Count()==decimal.Zero)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> OrderCreate([FromBody] OrderCreateCommand orderCreateCommand)
        {
            var result = await _mediator.Send(orderCreateCommand);

            return Ok(result);
        }
    }
}
