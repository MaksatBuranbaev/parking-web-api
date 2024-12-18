using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Services
{
    public class ParkingService: IParkingService
    {
        private readonly AppDbContext _context;
        public ParkingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Parking>> GetParkingsAsync()
        {
            return await _context.Parkings
                .ToListAsync();
        }

        public async Task<IEnumerable<Parking>> GetParkingByIdAsync(Guid id)
        {
            return await _context.Parkings
                .Where(p => p.Id == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByParkingAsync(Guid id)
        {
            return await _context.Parkings
                .Where(p => p.Id == id)
                .SelectMany(p => p.Users)
                .ToListAsync();
        }

        public async Task<int> GetNumberFloor(Guid id)
        {
            var max = await _context.Parkings
                .Where(p => p.Id == id)
                .SelectMany(p => p.ParkingSpaces)
                .MaxAsync(e => e.Floor);

            var min = await _context.Parkings
                .Where(p => p.Id == id)
                .SelectMany(p => p.ParkingSpaces)
                .MinAsync(e => e.Floor);

            return Math.Abs(max - min) + 1;
        }

        public async Task<int> GetNumberParkingSpacesAsync(Guid id, int number)
        {
            return await _context.Parkings
                .Where(p => p.Id == id)
                .SelectMany(p => p.ParkingSpaces)
                .Where(p => p.Floor == number)
                .CountAsync();
        }

        public async Task<Parking> PostParkingAsync(ParkingDto dto)
        {
            var parking = new Parking()
            {
                Id = new Guid(),
                Address = dto.Address
            };

            _context.Parkings.Add(parking);
            await _context.SaveChangesAsync();

            return parking;
        }

        public async Task<bool> DeleteParkingAsync(Guid id)
        {
            var parking = await _context.Parkings.FindAsync(id);

            if (parking == null)
            {
                return false;
            }

            _context.Parkings.Remove(parking);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
