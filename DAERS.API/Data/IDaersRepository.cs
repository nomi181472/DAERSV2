using System.Collections.Generic;
using System.Threading.Tasks;
using DAERS.API.Models;

namespace DAERS.API.Data
{
    public interface IDaersRepository
    {
         void Add<T>(T entity) where T:class; //check
         void Delete<T>(T entity)where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}