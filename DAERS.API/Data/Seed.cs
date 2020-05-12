using System.Collections.Generic;
using DAERS.API.Models;
using Newtonsoft.Json;

namespace DAERS.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;

        }        public void SeedUsers()
        {
            var userData=System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users=JsonConvert.DeserializeObject<List<User>>(userData);
            foreach(var user in users)
            {
                byte[] ph,ps;
                CreatePasswordHash("password",out ph,out ps);
                user.PasswordHash=ph;
                user.PasswordSalt=ps;
                user.UserName=user.UserName.ToLower();
                _context.Users.Add(user);
                _context.SaveChanges();


            }


        }
         private void CreatePasswordHash(string password, out byte[] passwordH, out byte[] passwordS)
        {
          using(var hmac=new System.Security.Cryptography.HMACSHA512() )  
          {
              passwordS=hmac.Key;
              passwordH=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
          }
        }

    }
}