using System.Threading.Tasks;
using LD_4_Interneto_tech.Models;

namespace LD_4_Interneto_tech.Interfaces
{
    public interface IUserRepository
    {
         Task<User> Authenticate(string userName, string password);   
         void Register(string userName, string password, string email, string mobileNumber); 
         Task<bool> UserAlreadyExists(string userName);
        Task<User> GetUserByResetToken(string resetToken);
        Task<User> GetUserByUsername(string userName);
        void HashPassword(string newPassword, out byte[] passwordHash, out byte[] passwordKey);
        Task SaveAsync();
        
    }
}