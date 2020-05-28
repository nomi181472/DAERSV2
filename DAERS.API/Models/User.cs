using System;
using System.Collections.Generic;

namespace DAERS.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive   { get; set; }
        public string Introduction { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public float Waist { get; set; }
        public float Lats { get; set; }

        public float Bmi { get; set; }
        public string Category { get; set; }
        public string Email { get; set; }
        public ICollection<PhotoU> Photos { get; set; }
        public ICollection<Like> Likers { get; set; }
        public ICollection<Like> Likees { get; set; }
        


       
        
    }
}