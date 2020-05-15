namespace DAERS.API.Models
{
    public class PhotoE
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string PublicEId { get; set; }
        public Exercise   Exercise { get; set; }
        public int ExerciseId { get; set; }
        
    }
}