using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDto dto)
        {
            if (dto.Start == DateTime.MinValue) 
            {
                return BadRequest("Время начала использование парковки обязательно");
            }

            var order = new Order()
            {
                Id = new Guid(),
                UserId = dto.UserId,
                ParkingSpaceId = dto.ParkingSpaceId,
                Start = dto.Start,
                End = dto.End
            };

            var orderHistory = new OrderHistory()
            {
                Id = new Guid(),
                UserId = dto.UserId,
                ParkingSpaceId = dto.ParkingSpaceId,
                Start = dto.Start,
                End = dto.End
            };

            await _context.ParkingSpaces
                .Where(o => o.Id == dto.ParkingSpaceId)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(c => c.IsFree, false));

            _context.Orders.Add(order);
            _context.OrdersHistory.Add(orderHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostOrder", order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _context.ParkingSpaces
                .Where(o => o.Id == order.ParkingSpaceId)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(c => c.IsFree, true));

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .ToListAsync();
        }

        [HttpGet("History")]
        public async Task<ActionResult<IEnumerable<OrderHistory>>> GetOrderHistory()
        {
            return await _context.OrdersHistory
                .ToListAsync();
        }

        [HttpDelete("History")]
        public async Task<IActionResult> DeleteOrderHistory()
        {

            await _context.OrdersHistory.ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrder(Guid id, DateTime dt)
        {
            await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteUpdateAsync(s => 
                    s.SetProperty(c => c.End, dt));

            return NoContent();
        }
    }
}
