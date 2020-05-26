using System;

namespace DAERS.API.Dtos.DtosForExercise
{
    public class PhotosEForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
         public bool IsMain { get; set; }
        public string Description { get; set; }
        public string PublicEId { get; set; }
    }
}