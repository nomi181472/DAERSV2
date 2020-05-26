using System.ComponentModel.DataAnnotations;

namespace DAERS.API.Dtos
{
    public class ExerciseForListDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string Type { get; set; } //Bodyonly etc
       
        public string UsedFor { get; set; } //Strength etc
        
        public string Category { get; set; } //N M H
      
        public int Level { get; set; } //0
        public string PhotoEUrl { get; set; }
        
    }
}