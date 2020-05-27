using System.Collections.Generic;
using System.Threading.Tasks;
using DAERS.API.Helpers.Paging;
using DAERS.API.Models;

namespace DAERS.API.Data
{
    public interface IDaersRepository
    {
         void Add<T>(T entity) where T:class; //check
         void Delete<T>(T entity)where T:class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UParams uParams);
         Task<User> GetUser(int id);
         Task<PhotoU> GetPhoto(int id);
         
         Task<PhotoU> GetMainPhotoForUser(int userId);
    }
}