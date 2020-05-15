using System.Collections.Generic;

namespace DAERS.API.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } //Bodyonly etc
        public string UsedFor { get; set; } //Strength etc
        public string Category { get; set; } //N M H
        public int Level { get; set; } //0
        public ICollection<PhotoE> PhotosE { get; set; }
    }
}
   