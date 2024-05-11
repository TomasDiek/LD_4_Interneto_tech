using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LD_4_Interneto_tech.Interfaces;
using LD_4_Interneto_tech.Models;
using LD_4_Interneto_tech.Data;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace LD_4_Interneto_tech.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;
        public UserRepository(DataContext dc)
        {
            this.dc = dc;

        }
        public async Task<User> Authenticate(string userName, string passwordText)
        {
            var user =  await dc.Users.FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null || user.PasswordKey == null)
                return null;

            if (!MatchPasswordHash(passwordText, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i=0; i<passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;
            }            
        }

        public void Register(string userName, string password, string email, string mobileNumber)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

            User user = new User();
            user.Username = userName;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            user.Email = email;
            user.MobileNumber= mobileNumber;

            dc.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await dc.Users.AnyAsync(x => x.Username == userName);
        }
        //password  reset
        public async Task<User> GetUserByResetToken(string resetToken)
        {
            return await dc.Users.FirstOrDefaultAsync(u => u.ResetToken == resetToken);
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            return await dc.Users.FirstOrDefaultAsync(u => u.Username == userName);
        }
        public void HashPassword(string password, out byte[] passwordHash, out byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task SaveAsync()
        {
            await dc.SaveChangesAsync();
        }
    }
}