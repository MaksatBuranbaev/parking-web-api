using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;
using ParkingWebApi.Services;
using System.Xml.Linq;

namespace ParkingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpacesController : ControllerBase
    {
        private readonly IParkingSpaceService _parkingSpaceService;
        public ParkingSpacesController(IParkingSpaceService parkingSpaceService)
        {
            _parkingSpaceService = parkingSpaceService;
        }

        [HttpGet("Free")]
        public async Task<ActionResult<IEnumerable<ParkingSpace>>> GetFree()
        {
            var result = await _parkingSpaceService.GetFreeAsync();

            return Ok(result);
        }

        [HttpGet("Occupied")]
        public async Task<ActionResult<IEnumerable<ParkingSpace>>> GetOccupied()
        {
            var result = await _parkingSpaceService.GetOccupiedAsync();

            return Ok(result); ;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ParkingSpace>>> GetDescription(Guid id)
        {
            var description = await _parkingSpaceService.GetDescriptionAsync(id);

            return Ok(description);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingSpace>> PostParkingSpace(ParkingSpaceDto dto)
        {
            var parkingSpace = await _parkingSpaceService.PostParkingSpaceAsync(dto);

            return CreatedAtAction("PostParkingSpace", parkingSpace);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingSpace(Guid id)
        {
            var result = await _parkingSpaceService.DeleteParkingSpaceAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
