using System.Collections.Generic;
using System.Threading.Tasks;
using DAERS.API.Models;

namespace DAERS.API.Data
{
    public interface INutritionFactRepository
    {
         void AddN<T>(T entity) where T:class; //check
         void DeleteN<T>(T entity)where T:class;
         Task<bool> SaveAllN();
         Task<IEnumerable<NutritionFact>> GetListNutritionFacts();
         Task<NutritionFact> GetNutritionFact(int id);
         Task<PhotoN> GetPhotoN(int id);
         Task<NutritionFact> AddNutritionFact(NutritionFact nutritionFact);
         
         Task<bool> NutritionFactExists(string nutritionFactName);
         Task<PhotoN> GetMainPhotoForNutritionFact(int nutritionFactId);
    }
}