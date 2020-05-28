using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAERS.API.Helpers.Paging;
using DAERS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DAERS.API.Data
{
    public class DaersRepository : IDaersRepository
    {
        public DataContext _context { get; }
        public DaersRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user=await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.Id==id);
            return user;
        }
      public async  Task<PagedList<User>> GetUsers(UParams uParams){
          var users=_context.Users.Include(p=>p.Photos).OrderByDescending(u=>u.LastActive).AsQueryable();
            users=users.Where(u=>u.Id!=uParams.UserId);
            // category will be implemeted
            if(uParams.likees)
            {
                var usersLikees=await GetUserLikes(uParams.UserId,uParams.likers);
                users=users.Where(x=>usersLikees.Contains(x.Id));
            }
            if(uParams.likers)
            {
                   var usersLikers=await GetUserLikes(uParams.UserId,uParams.likers);
                users=users.Where(x=>usersLikers.Contains(x.Id));
            }
            if(uParams.MinAge!=18 || uParams.MaxAge!=99)
            {
                users=users.Where(u=>u.Age>=uParams.MinAge && u.Age<=uParams.MaxAge);
            }
            if(!string.IsNullOrEmpty(uParams.OrderBy))
            {
                switch (uParams.OrderBy)
                {
                    case "created":
                    users=users.OrderByDescending(u=>u.Created);
                    break;
                    default:
                    users=users.OrderByDescending(u=>u.LastActive);
                    break;

                }
            }
            
          return await PagedList<User>.CreateAsync(users,uParams.PageNumber,uParams.PageSize);
        }
        private async Task<IEnumerable<int>> GetUserLikes(int id,bool likers)
        {
            var user=await _context.Users.Include(x=>x.Likers).Include(x=>x.Likees)
            .FirstOrDefaultAsync(x=>x.Id==id);
            if(likers)
            {
                return user.Likers.Where(x=>x.LikeeId==id).Select(x=>x.LikerId);
            }
            else
            {
                return user.Likees.Where(x=>x.LikerId==id).Select(x=>x.LikeeId);
            }
        }


        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<PhotoU> GetPhoto(int id)
        {
            var photo= await _context.Photos.FirstOrDefaultAsync(p => p.Id==id);
            return photo;
        }

        public async Task<PhotoU> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u=>u.UserId==userId).FirstOrDefaultAsync(p=>p.IsMain);
        }

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(
                u=>u.LikerId==userId && u.LikeeId==recipientId);
        }
    }
}