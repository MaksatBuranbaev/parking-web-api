namespace ParkingWebApi.Models.Enteties
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid ParkingId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public ICollection<UserParkingPreference> UserParkingPreference { get; set; } = [];
        public string Position { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public Parking Parking { get; set; } = null!;
    }
}
