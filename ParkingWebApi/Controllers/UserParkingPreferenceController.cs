using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserParkingPreferenceController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserParkingPreferenceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostPrefenceParkingSpace(UserParkingPreferenceDto dto)
        {
            var upp = new UserParkingPreference()
            {
                Id = new Guid(),
                UserId = dto.UserId,
                ParkingSpaceId = dto.ParkingSpaceId,
                Preference_Level = dto.Preference_Level
            };

            _context.UserParkingPreferences.Add(upp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostPrefenceParkingSpace", upp);
        }

        [HttpGet("Favorites/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetFavorites(Guid id)
        {
            var favorites = await _context.UserParkingPreferences
                .Where(u => u.UserId == id && u.Preference_Level > 0)
                .ToListAsync();
            
            return Ok(favorites);
        }

        [HttpGet("Unfavorites/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUnfavorites(Guid id)
        {
            var favorites = await _context.UserParkingPreferences
                .Where(u => u.UserId == id && u.Preference_Level <= 0)
                .ToListAsync();

            return Ok(favorites);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(Guid id)
        {
            var favorite = await _context.UserParkingPreferences
                .Where(u => u.UserId == id && u.Preference_Level >= 0)
                .OrderByDescending(u => u.Preference_Level)
                .FirstOrDefaultAsync();

            if (favorite == null)
            {
                return NotFound();
            }

            return Ok(favorite);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserParkingPreference(Guid id)
        {
            var upp = await _context.UserParkingPreferences.FindAsync(id);
            if (upp == null)
            {
                return NotFound();
            }

            _context.UserParkingPreferences.Remove(upp);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
