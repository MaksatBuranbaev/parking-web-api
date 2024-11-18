using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Models.Dto
{
    public class UserParkingPreferenceDto
    {
        public Guid UserId { get; set; }
        public Guid ParkingSpaceId { get; set; }
        public int Preference_Level { get; set; }
    }
}
