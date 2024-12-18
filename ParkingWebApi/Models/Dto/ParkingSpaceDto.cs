namespace ParkingWebApi.Models.Dto
{
    public class ParkingSpaceDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public Guid ParkingId { get; set; }
        public int Floor { get; set; }
    }
}
