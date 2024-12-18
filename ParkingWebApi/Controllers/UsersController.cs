using Microsoft.AspNetCore.Http;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var result = await _userService.GetUsersAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("{firstName} {lastName}")]
        public async Task<ActionResult<IEnumerable<User>>> GeUsersByName(string firstName, string lastName)
        {
            var result = await _userService.GetUsersByNameAsync(firstName, lastName);

            return Ok(result);

        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDto dto)
        {
            var user = await _userService.PostUserAsync(dto);

            return CreatedAtAction("PostUser", user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result) 
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}/position")]
        public async Task<IActionResult> PatchUserPosition(Guid id, string position)
        {
            await _userService.PatchUserPositionAsync(id, position);

            return NoContent();
        }

        [HttpPatch("{id}/city")]
        public async Task<IActionResult> PatchUserCity(Guid id, string city)
        {
            await _userService.PatchUserCityAsync(id, city);

            return NoContent();
        }
    }
}
