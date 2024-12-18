using Microsoft.AspNetCore.Mvc;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;
using ParkingWebApi.Services;

namespace ParkingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParkings()
        {
            var result = await _parkingService.GetParkingsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParkingById(Guid id)
        {
            var result = await _parkingService.GetParkingByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("employees/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByParking(Guid id)
        {
            var result = await _parkingService.GetUsersByParkingAsync(id);

            return Ok(result);
        }

        [HttpGet("floor/{id}")]
        public async Task<ActionResult<int>> GetNumberFloor(Guid id)
        {
            var result = await _parkingService.GetNumberFloor(id);

            return Ok(result);
        }

        [HttpGet("number-parking-spaces/{id}")]
        public async Task<ActionResult<int>> GetNumberParkingSpaces(Guid id, int number)
        {
            var result = await _parkingService.GetNumberParkingSpacesAsync(id, number);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Parking>> PostParking(ParkingDto dto)
        {
            var parking = await _parkingService.PostParkingAsync(dto);

            return CreatedAtAction("PostParking", parking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _parkingService.DeleteParkingAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
