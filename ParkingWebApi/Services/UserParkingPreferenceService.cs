using Microsoft.AspNetCore.Mvc;
using ParkingWebApi.Data;
using ParkingWebApi.Models.Dto;
using ParkingWebApi.Models.Enteties;
using Microsoft.EntityFrameworkCore;

namespace ParkingWebApi.Services
{
    public class UserParkingPreferenceService: IUserParkingPreferenceService
    {
        private readonly AppDbContext _context;
        public UserParkingPreferenceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserParkingPreference> PostPrefenceParkingSpaceAsync(UserParkingPreferenceDto dto)
        {
            var upp = new UserParkingPreference()
            {
                Id = new Guid(),
                UserId = dto.UserId,
                ParkingSpaceId = dto.ParkingSpaceId,
                Preference_Level = dto.Preference_Level
            };

            _context.UserParkingPreferences.Add(upp);
            await _context.SaveChangesAsync();

            return upp;
        }

        public async Task<IEnumerable<UserParkingPreference>> GetFavoritesAsync(Guid id)
        {
            var favorites = await _context.UserParkingPreferences
                .Where(u => u.UserId == id && u.Preference_Level > 0)
                .ToListAsync();

            return favorites;
        }

        public async Task<IEnumerable<UserParkingPreference>> GetUnfavoritesAsync(Guid id)
        {
            var unfavorites = await _context.UserParkingPreferences
                .Where(u => u.UserId == id && u.Preference_Level <= 0)
                .ToListAsync();

            return unfavorites;
        }

        public async Task<UserParkingPreference> GetPreferenceParkingSpaceAsync(Guid id)
        {
            var favorite = await _context.UserParkingPreferences
                .Where(u => u.UserId == id && u.Preference_Level >= 0 && u.ParkingSpace.IsFree != false)
                .OrderByDescending(u => u.Preference_Level)
                .FirstOrDefaultAsync();

            return favorite;
        }

        public async Task<bool> DeleteUserParkingPreferenceAsync(Guid id)
        {
            var upp = await _context.UserParkingPreferences.FindAsync(id);
            if (upp == null)
            {
                return false;
            }

            _context.UserParkingPreferences.Remove(upp);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
