using DAERS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DAERS.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<PhotoU> Photos { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<PhotoE> PhotosEx { get; set; }
        public DbSet<NutritionFact>   NutritionFacts { get; set; }
        public DbSet<PhotoN> PhotosNF { get; set; }

    }
}