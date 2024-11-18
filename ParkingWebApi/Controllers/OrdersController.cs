using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;
using ParkingWebApi.Services;

namespace ParkingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDto dto)
        {
            if (dto.Start == DateTime.MinValue) 
            {
                return BadRequest("Время начала использование парковки обязательно");
            }

            var order = await _orderService.PostOrderAsync(dto);

            return CreatedAtAction("PostOrder", order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderService.DeleteOrderAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var result = await _orderService.GetOrdersAsync();

            return Ok(result);
        }

        [HttpGet("History")]
        public async Task<ActionResult<IEnumerable<OrderHistory>>> GetOrderHistory()
        {
            var result = await _orderService.GetOrderHistoryAsync();

            return Ok(result);
        }

        [HttpDelete("History")]
        public async Task<IActionResult> DeleteOrderHistory()
        {

            await _orderService.DeleteAllOrderHistoryAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrder(Guid id, DateTime dt)
        {
            await _orderService.PatchOrderAsync(id, dt);

            return NoContent();
        }
    }
}
