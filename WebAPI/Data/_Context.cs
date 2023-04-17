using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class _Context : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public _Context(DbContextOptions<_Context> options) 
            :base(options)
        {
        }
    }
}
