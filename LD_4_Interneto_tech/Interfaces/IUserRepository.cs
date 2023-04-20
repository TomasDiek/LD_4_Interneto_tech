using System.Threading.Tasks;
using LD_4_Interneto_tech.Models;

namespace LD_4_Interneto_tech.Interfaces
{
    public interface IUserRepository
    {
         Task<User> Authenticate(string userName, string password);   
         void Register(string userName, string password); 

         Task<bool> UserAlreadyExists(string userName);
    }
}