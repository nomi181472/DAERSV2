using Microsoft.EntityFrameworkCore;

namespace DAERS.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
    }
}