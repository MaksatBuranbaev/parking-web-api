namespace ParkingWebApi.Models.Dto
{
    public class UserDto
    {
        public Guid ParkingId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}
