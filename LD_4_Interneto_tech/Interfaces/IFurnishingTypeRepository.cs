using System.Collections.Generic;
using System.Threading.Tasks;
using LD_4_Interneto_tech.Models;

namespace LD_4_Interneto_tech.Interfaces
{
    public interface IFurnishingTypeRepository
    {
         Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync();
    }
}