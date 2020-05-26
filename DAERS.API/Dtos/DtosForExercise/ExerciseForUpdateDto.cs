namespace DAERS.API.Dtos.DtosForExercise
{
    public class ExerciseForUpdateDto
    {
        
        public string Name { get; set; }
        public string Type { get; set; } //Bodyonly etc
        public string UsedFor { get; set; } //Strength etc
        public string Category { get; set; } //N M H
        public int Level { get; set; } //0

    }

}
