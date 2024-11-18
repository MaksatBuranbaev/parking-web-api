using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;
using ParkingWebApi.Services;

namespace ParkingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserParkingPreferenceController : ControllerBase
    {
        private readonly IUserParkingPreferenceService _userParkingPreferenceService;
        public UserParkingPreferenceController(IUserParkingPreferenceService userParkingPreferenceService)
        {
            _userParkingPreferenceService = userParkingPreferenceService;
        }

        [HttpPost]
        public async Task<IActionResult> PostPrefenceParkingSpace(UserParkingPreferenceDto dto)
        {
            var upp = await _userParkingPreferenceService.PostPrefenceParkingSpaceAsync(dto);

            return CreatedAtAction("PostPrefenceParkingSpace", upp);
        }

        [HttpGet("Favorites/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetFavorites(Guid id)
        {
            var favorites = await _userParkingPreferenceService.GetFavoritesAsync(id);
            
            return Ok(favorites);
        }

        [HttpGet("Unfavorites/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUnfavorites(Guid id)
        {
            var favorites = await _userParkingPreferenceService.GetUnfavoritesAsync(id);

            return Ok(favorites);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetPreferenceParkingSpace(Guid id)
        {
            var favorite = await _userParkingPreferenceService.GetPreferenceParkingSpaceAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            return Ok(favorite);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserParkingPreference(Guid id)
        {
            var result = await _userParkingPreferenceService.DeleteUserParkingPreferenceAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
