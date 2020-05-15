using System.Collections.Generic;
using System.Threading.Tasks;
using DAERS.API.Models;

namespace DAERS.API.Data
{
    public interface IExerciseRepository
    {
        void AddE<T>(T entity) where T:class; //check
         void DeleteE<T>(T entity)where T:class;
         Task<bool> SaveAllE();
         Task<IEnumerable<Exercise>> GetListExercises();
         Task<Exercise> GetExercise(int id);
         Task<PhotoE> GetPhotoE(int id);
         Task<Exercise> AddExercise(Exercise exercise);
         Task<bool> ExerciseExists(string exerciseName);
    }
}