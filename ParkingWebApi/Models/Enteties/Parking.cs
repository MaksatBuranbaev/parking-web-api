namespace ParkingWebApi.Models.Enteties
{
    public class Parking
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public ICollection<ParkingSpace> ParkingSpaces { get; set; } = [];
        public ICollection<User> Users { get; set; } = [];
    }
}
