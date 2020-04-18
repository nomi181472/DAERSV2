using System;
using System.Threading.Tasks;
using DAERS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DAERS.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext Context;
        public AuthRepository(DataContext context)
        {
            Context = context;

        }
        public  async Task<User> Login(string username, string password)
        {
            var user= await Context.Users.FirstOrDefaultAsync(x=>x.UserName==username);
            if (user==null)
                return null;
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt) )  
          {
            
             var  ComputedpasswordH=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i=0;i<ComputedpasswordH.Length;i++)
                {
                    if(ComputedpasswordH[i]!=passwordHash[i])
                    return false;
                }
          }
          return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordH,passwordS;
            CreatePasswordHash(password,out passwordH,out passwordS);
            user.PasswordHash=passwordH;
            user.PasswordSalt=passwordS;
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordH, out byte[] passwordS)
        {
          using(var hmac=new System.Security.Cryptography.HMACSHA512() )  
          {
              passwordS=hmac.Key;
              passwordH=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
          }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await Context.Users.AnyAsync(x=>x.UserName==username))
            return true;
            return false;
        }
    }
}