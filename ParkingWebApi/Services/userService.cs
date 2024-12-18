using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Services
{
    public class UserService: IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByNameAsync(string firstName, string lastName)
        {
            return await _context.Users
                .Where(u => u.FirstName == firstName && u.LastName == lastName)
                .ToListAsync();
        }

        public async Task<User> PostUserAsync(UserDto dto)
        {
            var user = new User()
            {
                Id = new Guid(),
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                Position = dto.Position,
                City = dto.City,
                ParkingId = dto.ParkingId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task PatchUserPositionAsync(Guid id, string position)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(c => c.Position, position));
        }

        public async Task PatchUserCityAsync(Guid id, string city)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(c => c.City, city));
        }
    }
}
