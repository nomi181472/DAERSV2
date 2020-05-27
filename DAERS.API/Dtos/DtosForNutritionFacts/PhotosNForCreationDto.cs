using System;
using Microsoft.AspNetCore.Http;

namespace DAERS.API.Dtos.DtosForNutritionFacts
{
    public class PhotosNForCreationDto
    {
         public string Url { get; set; }
        public IFormFile File {get;set;}
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
       public string PublicNId { get; set; }
        public PhotosNForCreationDto()
        {
            DateAdded=DateTime.Now;
        }
    }
}