using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Services
{
    public interface IParkingService
    {
        Task<bool> DeleteParkingAsync(Guid id);
        Task<IEnumerable<User>> GetUsersByParkingAsync(Guid id);
        Task<int> GetNumberFloor(Guid id);
        Task<int> GetNumberParkingSpacesAsync(Guid id, int number);
        Task<IEnumerable<Parking>> GetParkingByIdAsync(Guid id);
        Task<IEnumerable<Parking>> GetParkingsAsync();
        Task<Parking> PostParkingAsync(ParkingDto dto);
    }
}