using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingWebApi.Models.Enteties
{
    public class UserParkingPreference
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid ParkingSpaceId { get; set; }
        public ParkingSpace ParkingSpace { get; set; } = null!;
        public int Preference_Level { get; set; }
    }
}