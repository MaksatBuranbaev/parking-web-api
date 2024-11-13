namespace ParkingWebApi.Models.Enteties
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserParkingPreference> UserParkingPreference { get; set; } = [];
    }
}
