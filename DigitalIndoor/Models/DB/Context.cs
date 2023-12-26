using DigitalIndoor.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalIndoor.Models.DB
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }  
        public DbSet<Order> Orders { get; set; } 
        public DbSet<PlayList> Playlists { get; set; }
        public DbSet<PlayListsInOrder> PlayListsInOrder { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Video> Videos { get; set; }

    }
}
