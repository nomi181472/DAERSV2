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
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
            .HasKey(k=>new {k.LikerId,k.LikeeId});
            builder.Entity<Like>()
            .HasOne(u=>u.Likee)
            .WithMany(u=>u.Likers)
            .HasForeignKey(u=>u.LikeeId)
            .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Like>()
            .HasOne(u=>u.Liker)
            .WithMany(u=>u.Likees)
            .HasForeignKey(u=>u.LikerId)
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
}