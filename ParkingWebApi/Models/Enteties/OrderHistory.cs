using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Models.Enteties
{
    public class OrderHistory
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ParkingSpaceId { get; set; }
        public User User { get; set; } = null!;
        public ParkingSpace ParkingSpace { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
