using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}