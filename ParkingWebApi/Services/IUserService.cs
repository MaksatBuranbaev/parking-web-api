using Microsoft.AspNetCore.Mvc;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> PostUserAsync(UserDto dto);
    }
}