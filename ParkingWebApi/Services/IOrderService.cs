using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Services
{
    public interface IOrderService
    {
        Task DeleteAllOrderHistoryAsync();
        Task<bool> DeleteOrderAsync(Guid id);
        Task<IEnumerable<OrderHistory>> GetOrderHistoryAsync();
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task PatchOrderAsync(Guid id, DateTime dt);
        Task<Order> PostOrderAsync(OrderDto dto);
    }
}