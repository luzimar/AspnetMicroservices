using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.ViewModels;
using Ordering.Application.Contracts.Notifications;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : MainController
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator, INotificationService notificationService) : base(notificationService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(ResponseViewModelFull<OrdersVm>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseViewModelFull<OrdersVm>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseViewModelFull<OrdersVm>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName);
            var orders = await _mediator.Send(query);
            return CustomResponseFull(results: orders);
        }

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseViewModelBasic>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            await _mediator.Send(command);
            return CustomResponseBasic();
        }

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseViewModelBasic>> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return CustomResponseBasic();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseViewModelBasic>> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);
            return CustomResponseBasic();
        }
    }
}
