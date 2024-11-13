using Microsoft.EntityFrameworkCore;
using ParkingWebApi.Models.Enteties;

namespace ParkingWebApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrdersHistory { get; set; }
        public DbSet<UserParkingPreference> UserParkingPreferences { get; set; }
    }
}
