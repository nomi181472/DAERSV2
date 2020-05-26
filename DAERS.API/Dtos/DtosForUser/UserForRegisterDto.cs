using System;
using System.ComponentModel.DataAnnotations;

namespace DAERS.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="You must specify password between 4 and 8 characters")]
        public  string Password {get;set;}
     [Required]
        public string Gender { get; set; }
         [Required]
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive   { get; set; }
        [Required]
        public string City { get; set; }
         [Required]
        public string Country { get; set; }
         [Required]
        public float Height { get; set; }
         [Required]
        public float Weight { get; set; }
         [Required]
        public float Waist { get; set; }
         [Required]
        public float Lats { get; set; }
         [Required]

        public string Email { get; set; }
        public UserForRegisterDto()
         {
            Created=DateTime.Now;
            LastActive=DateTime.Now;
         }
      
    }
}