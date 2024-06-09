using System.Collections.Generic;
using System.Threading.Tasks;
using LD_4_Interneto_tech.Models;

namespace LD_4_Interneto_tech.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent);
        Task<Property> GetPropertyDetailAsync(int id);
        Task<Property> GetPropertyByIdAsync(int id);
        void AddProperty(Property property);
        void DeleteProperty(int id);
        Task<bool> UpdateProperty(int id, Property property);
        Task<int?> GetUserIdByPropertyIdAsync(int propertyId);
        Task<IEnumerable<Property>> SearchPropertiesAsync(string searchTerm, int sellRent);
        Task<IEnumerable<Property>> GetPropertiesByUserIdAsync(int userId);
    }
}