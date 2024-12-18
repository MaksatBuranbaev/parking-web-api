using Microsoft.AspNetCore.Mvc;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;
using Microsoft.EntityFrameworkCore;

namespace ParkingWebApi.Services
{
    public class OrderService: IOrderService
    {
        private readonly AppDbContext _context;
        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> PostOrderAsync(OrderDto dto)
        {
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

            return order;
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            await _context.ParkingSpaces
                .Where(o => o.Id == order.ParkingSpaceId)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(c => c.IsFree, true));

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderHistory>> GetOrderHistoryAsync()
        {
            return await _context.OrdersHistory
                .ToListAsync();
        }

        public async Task DeleteAllOrderHistoryAsync()
        {
            await _context.OrdersHistory.ExecuteDeleteAsync();
        }

        public async Task PatchOrderAsync(Guid id, DateTime dt)
        {
            await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(c => c.End, dt));
        }
    }
}
