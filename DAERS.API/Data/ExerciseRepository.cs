using System.Collections.Generic;
using System.Threading.Tasks;
using DAERS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DAERS.API.Data
{
    public class ExerciseRepository : IExerciseRepository
    {
        public DataContext _context { get; }
        public ExerciseRepository(DataContext context)
        {
            _context = context;

        }

        public void AddE<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void DeleteE<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAllE()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<IEnumerable<Exercise>> GetListExercises()
        {
            var exercises=await this._context.Exercises.Include(p=>p.PhotosE).ToListAsync();
            return exercises;
        }
        public async Task<Exercise> GetExercise(int id)
        {
            var exercise=await this._context.Exercises.Include(p=>p.PhotosE).FirstOrDefaultAsync(p=>p.Id==id);
            return exercise;
        }

        public async Task<PhotoE> GetPhotoE(int id)
        {
            var photo= await _context.PhotosEx.FirstOrDefaultAsync(p => p.Id==id);
            return photo;
        }

        public async Task<bool> ExerciseExists(string exerciseName)
        {
         if(await this._context.Exercises.AnyAsync(p=>p.Name==exerciseName))
         return true;
         return false;
        }

        public async Task<Exercise> AddExercise(Exercise exercise)
        {
            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }
    }
}