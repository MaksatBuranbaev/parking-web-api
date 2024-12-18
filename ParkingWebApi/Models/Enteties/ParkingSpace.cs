using System.ComponentModel.DataAnnotations;

namespace ParkingWebApi.Models.Enteties
{
    public class ParkingSpace
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public bool IsFree { get; set; } = true;
        public ICollection<UserParkingPreference> UserPreferences { get; set; } = [];
        public Parking Parking { get; set; } = null!;
        public Guid ParkingId { get; set; }
        public int Floor { get; set; }
    }
}
