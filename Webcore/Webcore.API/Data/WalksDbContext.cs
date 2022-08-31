using Microsoft.EntityFrameworkCore;
using Webcore.API.Models.Domain;

namespace Webcore.API.Data
{
    public class WalksDbContext: DbContext
    {
        public WalksDbContext(DbContextOptions<WalksDbContext> options): base(options)
        {


        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }

    }
}
