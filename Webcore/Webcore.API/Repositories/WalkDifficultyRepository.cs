using Microsoft.EntityFrameworkCore;
using Webcore.API.Data;
using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly WalksDbContext walksDbContext;

        public WalkDifficultyRepository(WalksDbContext walksDbContext)
        {
            this.walksDbContext = walksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id=Guid.NewGuid();
            await walksDbContext.WalkDifficulties.AddAsync(walkDifficulty);
            await walksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await walksDbContext.WalkDifficulties.ToListAsync();
           
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await walksDbContext.WalkDifficulties.FirstAsync(x => x.Id == id);
            
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id,WalkDifficulty walkDifficulty)
        {
          var existWalkdiff= await walksDbContext.WalkDifficulties.FindAsync(id);
            if (existWalkdiff != null)
            {
                existWalkdiff.Code = walkDifficulty.Code;
                await walksDbContext.SaveChangesAsync();
                return existWalkdiff;
            }
            return null;
            
        }
    }
}
