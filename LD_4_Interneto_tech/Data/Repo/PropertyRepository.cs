using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LD_4_Interneto_tech.Interfaces;
using LD_4_Interneto_tech.Models;
using LD_4_Interneto_tech.Data;
using LD_4_Interneto_tech.Dto;

namespace LD_4_Interneto_tech.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext dc;

        public PropertyRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddProperty(Property property)
        {
            dc.Properties.Add(property);
        }

        public async void DeleteProperty(int id)
        {
            var property = dc.Properties.Find(id);
            dc.Properties.Remove(property);

        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            var properties = await dc.Properties
            .Include(p => p.PropertyType)
            .Include(p => p.City)
            .Include(p => p.FurnishingType)
            .Include(p => p.Photos)
            .Where(p => p.SellRent == sellRent)
            .ToListAsync();
               
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var properties = await dc.Properties
            .Include(p => p.PropertyType)
            .Include(p => p.City)
            .Include(p => p.FurnishingType)
            .Include(p => p.Photos)
            .Where(p => p.Id == id)
            .FirstAsync();

            return properties;
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            var properties = await dc.Properties
            .Include(p => p.Photos)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

            return properties;
        }
        public async Task<bool> UpdateProperty(int id, Property property)
        {
           var propertyDb = await GetPropertyByIdAsync(id);

            if (property == null)
            {
                return false;
            }
            
            propertyDb.SellRent = property.SellRent;
            propertyDb.PropertyTypeId = property.PropertyTypeId;
            propertyDb.FurnishingTypeId = property.FurnishingTypeId;
            propertyDb.Price = property.Price;
            propertyDb.BuiltArea = property.BuiltArea;
            propertyDb.CityId = property.CityId;
            propertyDb.ReadyToMove = property.ReadyToMove;
            propertyDb.Address = property.Address;
            propertyDb.TotalFloors = property.TotalFloors;
            propertyDb.EstPossessionOn = property.EstPossessionOn;
            propertyDb.Age = property.Age;
            propertyDb.Description = property.Description;

            await dc.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<Property>> GetPropertiesByUserIdAsync(int userId)
        {
            var properties = await dc.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Include(p => p.Photos)
                .Where(p => p.PostedBy == userId)
                .ToListAsync();

            return properties;
        }
    }
}