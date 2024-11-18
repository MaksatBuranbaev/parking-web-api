using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Services
{
    public interface IParkingSpaceService
    {
        Task<bool> DeleteParkingSpaceAsync(Guid id);
        Task<IEnumerable<string>> GetDescriptionAsync(Guid id);
        Task<IEnumerable<ParkingSpace>> GetFreeAsync();
        Task<IEnumerable<ParkingSpace>> GetOccupiedAsync();
        Task<ParkingSpace> PostParkingSpaceAsync(ParkingSpaceDto dto);
    }
}