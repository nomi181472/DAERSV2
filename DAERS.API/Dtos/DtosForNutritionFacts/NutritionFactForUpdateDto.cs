namespace DAERS.API.Dtos.DtosForNutritionFacts
{
    public class NutritionFactForUpdateDto
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public double  Carbohydrate { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public double Calorie { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
    }
}