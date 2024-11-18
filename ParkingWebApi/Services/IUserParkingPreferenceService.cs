using Microsoft.AspNetCore.Mvc;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Services
{
    public interface IUserParkingPreferenceService
    {
        Task<bool> DeleteUserParkingPreferenceAsync(Guid id);
        Task<UserParkingPreference> GetPreferenceParkingSpaceAsync(Guid id);
        Task<IEnumerable<UserParkingPreference>> GetFavoritesAsync(Guid id);
        Task<IEnumerable<UserParkingPreference>> GetUnfavoritesAsync(Guid id);
        Task<UserParkingPreference> PostPrefenceParkingSpaceAsync(UserParkingPreferenceDto dto);
    }
}