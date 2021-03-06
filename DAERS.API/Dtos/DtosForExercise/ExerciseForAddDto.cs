using System.ComponentModel.DataAnnotations;

namespace DAERS.API.Dtos
{
    public class ExerciseForAddDto
    {
        public ExerciseForAddDto()
        {
            
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; } //Bodyonly etc
        [Required]
        public string UsedFor { get; set; } //Strength etc
        [Required]
        public string Category { get; set; } //N M H
        [Required]
        public int Level { get; set; } //0
    }
}