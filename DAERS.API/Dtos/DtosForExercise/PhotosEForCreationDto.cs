using System;
using Microsoft.AspNetCore.Http;

namespace DAERS.API.Dtos.DtosForExercise
{
    public class PhotosEForCreationDto
    {
        public string Url { get; set; }
        public IFormFile File {get;set;}
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
       public string PublicEId { get; set; }
        public PhotosEForCreationDto()
        {
            DateAdded=DateTime.Now;
        }
    }
}