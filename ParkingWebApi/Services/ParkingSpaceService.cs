using Microsoft.AspNetCore.Mvc;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;
using Microsoft.EntityFrameworkCore;

namespace ParkingWebApi.Services
{
    public class ParkingSpaceService: IParkingSpaceService
    {
        private readonly AppDbContext _context;
        public ParkingSpaceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParkingSpace>> GetFreeAsync()
        {
            return await _context.ParkingSpaces
                .Where(p => p.IsFree)
                .ToListAsync();
        }

        [HttpGet("Occupied")]
        public async Task<IEnumerable<ParkingSpace>> GetOccupiedAsync()
        {
            return await _context.ParkingSpaces
                .Where(p => p.IsFree == false)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<string>> GetDescriptionAsync(Guid id)
        {
            var description = await _context.ParkingSpaces
                .Where(p => p.Id == id)
                .Select(p => p.Description)
                .ToListAsync();

            return description;
        }

        [HttpPost]
        public async Task<ParkingSpace> PostParkingSpaceAsync(ParkingSpaceDto dto)
        {
            var parkingSpace = new ParkingSpace()
            {
                Id = new Guid(),
                Name = dto.Name,
                Description = dto.Description,
                Floor = dto.Floor,
                ParkingId = dto.ParkingId
            };

            _context.ParkingSpaces.Add(parkingSpace);
            await _context.SaveChangesAsync();

            return parkingSpace;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteParkingSpaceAsync(Guid id)
        {
            var parkingSpace = await _context.ParkingSpaces.FindAsync(id);
            if (parkingSpace == null)
            {
                return false;
            }

            _context.ParkingSpaces.Remove(parkingSpace);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
