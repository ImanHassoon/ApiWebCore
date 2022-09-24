using Microsoft.EntityFrameworkCore;
using Webcore.API.Models.Domain;

namespace Webcore.API.Data
{
    public class WalksDbContext: DbContext
    {
        public WalksDbContext(DbContextOptions<WalksDbContext> options): base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y=>y.UserRoles)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<User_Role>()
               .HasOne(x => x.User)
               .WithMany(y => y.UserRoles)
               .HasForeignKey(x => x.UserId);

        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }
        public  DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> user_Roles { get; set; }

    }
}
