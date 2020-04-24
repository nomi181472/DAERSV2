using System;
using System.Collections.Generic;
using DAERS.API.Models;

namespace DAERS.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive   { get; set; }
        public string Introduction { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public float Bmi { get; set; }
        public string Category { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }

    }
}