using DigitalIndoorAPI.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalIndoorAPI.Models.DB
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(x => x.PlayLists)
                .WithMany(x => x.Orders)
                .UsingEntity(typeof(PlayListsInOrder));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }  
        public DbSet<Order> Orders { get; set; } 
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<PlayListsInOrder> PlayListsInOrder { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<TerminalLog> TerminalLogs { get; set; }
        public DbSet<Video> Videos { get; set; }

    }
}
