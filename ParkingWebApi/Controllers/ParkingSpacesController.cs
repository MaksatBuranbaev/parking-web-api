using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;
using System.Xml.Linq;

namespace ParkingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpacesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ParkingSpacesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Free")]
        public async Task<ActionResult<IEnumerable<ParkingSpace>>> GetFree()
        {
            return await _context.ParkingSpaces
                .Where(p => p.IsFree)
                .ToListAsync();
        }

        [HttpGet("Occupied")]
        public async Task<ActionResult<IEnumerable<ParkingSpace>>> GetOccupied()
        {
            return await _context.ParkingSpaces
                .Where(p => p.IsFree == false)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ParkingSpace>>> GetDescription(Guid id)
        {
            var description = await _context.ParkingSpaces
                .Where(p => p.Id == id)
                .Select(p => p.Description)
                .ToListAsync();

            return Ok(description);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingSpace>> PostParkingSpace(ParkingSpaceDto dto)
        {
            var parkingSpace = new ParkingSpace()
            {
                Id = new Guid(),
                Name = dto.Name,
                Description = dto.Description
            };

            _context.ParkingSpaces.Add(parkingSpace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostParkingSpace", parkingSpace);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpace(Guid id)
        {
            var parkingSpace = await _context.ParkingSpaces.FindAsync(id);
            if (parkingSpace == null)
            {
                return NotFound();
            }

            _context.ParkingSpaces.Remove(parkingSpace);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
