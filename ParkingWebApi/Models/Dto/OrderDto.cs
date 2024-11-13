using System.ComponentModel.DataAnnotations;

namespace ParkingWebApi.Models.Dto
{
    public class OrderDto
    {
        public Guid UserId { get; set; }
        public Guid ParkingSpaceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
