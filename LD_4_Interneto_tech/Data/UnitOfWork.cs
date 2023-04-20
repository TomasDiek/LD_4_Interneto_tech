using LD_4_Interneto_tech.Data.Repo;
using LD_4_Interneto_tech.Interfaces;
using System.Threading.Tasks;

namespace LD_4_Interneto_tech.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }
        public ICityRepository CityRepository => 
            new CityRepository(dc);

        public IUserRepository UserRepository =>         
            new UserRepository(dc);

        public IFurnishingTypeRepository FurnishingTypeRepository =>         
            new FurnishingTypeRepository(dc);

        public IPropertyTypeRepository PropertyTypeRepository =>         
            new PropertyTypeRepository(dc);

        public IPropertyRepository PropertyRepository => 
            new PropertyRepository(dc);

        public async Task<bool> SaveAsync()
        {
           return await dc.SaveChangesAsync() > 0;
        }
    }
}