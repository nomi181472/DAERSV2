using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAERS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DAERS.API.Data
{
    public class NutritionFactRepository : INutritionFactRepository
    {
        private readonly DataContext _context;
        public NutritionFactRepository(DataContext context)
        {
            this._context = context;

        }
        public void AddN<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<NutritionFact> AddNutritionFact(NutritionFact nutritionFact)
        {
            await _context.NutritionFacts.AddAsync(nutritionFact);
            await _context.SaveChangesAsync();
            return nutritionFact;
        }

        public void DeleteN<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<NutritionFact>> GetListNutritionFacts()
        {
            var nutritionFact=await this._context.NutritionFacts.Include(p=>p.PhotosE).ToListAsync();
            return nutritionFact;
        }

        public async Task<PhotoN> GetMainPhotoForNutritionFact(int nutritionFactId)
        {
             return await _context.PhotosNF.Where(u=>u.NutritionFactId==nutritionFactId).FirstOrDefaultAsync(p=>p.IsMain);
        }

        public async Task<NutritionFact> GetNutritionFact(int id)
        {
            var nutritionFact=await this._context.NutritionFacts.Include(p=>p.PhotosE).FirstOrDefaultAsync(p=>p.Id==id);
            return nutritionFact;
        
        }

        public async Task<PhotoN> GetPhotoN(int id)
        {
             var photo= await _context.PhotosNF.FirstOrDefaultAsync(p => p.Id==id);
            return photo;
        }

        public async Task<bool> NutritionFactExists(string nutritionFactName)
        {
            if(await this._context.NutritionFacts.AnyAsync(p=>p.Name==nutritionFactName))
            return true;
            return false;
        }

        public async Task<bool> SaveAllN()
        {
            return await _context.SaveChangesAsync()>0;
        }
    }
}