using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LD_4_Interneto_tech.Interfaces;
using LD_4_Interneto_tech.Models;
namespace LD_4_Interneto_tech.Data.Repo
{
    public class UserFavoritePropertyRepository : IUserFavoritePropertyRepository
    {
        private readonly DataContext dc;

        public UserFavoritePropertyRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public async Task AddFavoriteProperty(int userId, int propertyId)
        {
            // Check if the user already has this property in favorites
            var existingFavorite = await dc.UserFavoriteProperty
                .FirstOrDefaultAsync(ufp => ufp.UserId == userId && ufp.PropertyId == propertyId);

            if (existingFavorite == null)
            {
                // If not, add the property to favorites
                var userFavoriteProperty = new UserFavoriteProperty
                {
                    UserId = userId,
                    PropertyId = propertyId
                };
                dc.UserFavoriteProperty.Add(userFavoriteProperty);
                await dc.SaveChangesAsync();
            }
        }

        public async Task RemoveFavoriteProperty(int userId, int propertyId)
        {
            // Find the favorite property to remove
            var favoriteProperty = await dc.UserFavoriteProperty
                .FirstOrDefaultAsync(ufp => ufp.UserId == userId && ufp.PropertyId == propertyId);

            if (favoriteProperty != null)
            {
                // Remove the favorite property
                dc.UserFavoriteProperty.Remove(favoriteProperty);
                await dc.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Property>> GetFavoriteProperties(int userId)
        {
            // Retrieve favorite property IDs for the user
            var favoritePropertyIds = await dc.UserFavoriteProperty
                .Where(ufp => ufp.UserId == userId)
                .Select(ufp => ufp.PropertyId)
                .ToListAsync();

            // Retrieve the actual property objects
            var favoriteProperties = await dc.Properties
                .Where(p => favoritePropertyIds.Contains(p.Id))
                .ToListAsync();

            return favoriteProperties;
        }
    }
}
