using System.Threading.Tasks;

namespace LD_4_Interneto_tech.Interfaces
{
    public interface IUnitOfWork
    {
         ICityRepository CityRepository {get; }

         IUserRepository UserRepository {get; }

         IPropertyRepository PropertyRepository {get; }

         IFurnishingTypeRepository FurnishingTypeRepository {get; }

         IPropertyTypeRepository PropertyTypeRepository {get; }

         Task<bool> SaveAsync();
    }
}