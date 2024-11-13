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
    }
}
