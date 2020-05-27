using System.ComponentModel.DataAnnotations;

namespace DAERS.API.Dtos.DtosForNutritionFacts
{
    public class NutritionFactForAddDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public double  Carbohydrate { get; set; }
        [Required]
        public double Protein { get; set; }
        [Required]
        public double Fats { get; set; }
        [Required]
        public double Calorie { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Type { get; set; }
    }
}