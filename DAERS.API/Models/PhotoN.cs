using System;

namespace DAERS.API.Models
{
    public class PhotoN
    {
        
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
         public bool IsMain { get; set; }
        public string PublicNId { get; set; }
        public NutritionFact  NutritionFact { get; set; }
        public int NutritionFactId { get; set; }
    }
}