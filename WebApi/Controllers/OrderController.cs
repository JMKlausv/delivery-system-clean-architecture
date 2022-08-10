using Application.Orders.Command.CreateOrder;
using Application.Orders.Command.DeleteOrder;
using Application.Orders.Command.UpdateOrder;
using Application.Orders.Query;
using Application.Orders.Query.GetOrders;
using Application.Orders.Query.GetSingleOrder;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiControllerBase
    {
        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IEnumerable<FetchOrderDto>> Get()
        {
            return await Mediator.Send(new GetOrdersQuery());
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<FetchOrderDto> Get(int id)
        {
            var query = new GetSingleOrderQuery
            {
                Id = id,
            };
            return await Mediator.Send(query);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteOrderCommand
            {
                Id = id,
            };
            return Ok(await Mediator.Send(command));
        }
    }
}
