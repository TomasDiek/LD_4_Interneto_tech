using LD_4_Interneto_tech.Models;

namespace LD_4_Interneto_tech.Interfaces
{
    public interface IUserFavoritePropertyRepository
    {
        Task AddFavoriteProperty(int userId, int propertyId);
        Task RemoveFavoriteProperty(int userId, int propertyId);
        Task<IEnumerable<Property>> GetFavoriteProperties(int userId);
    }

}
