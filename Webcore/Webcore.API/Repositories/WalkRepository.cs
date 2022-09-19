using Microsoft.EntityFrameworkCore;
using Webcore.API.Data;
using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalksDbContext walksDbContext;
        public WalkRepository(WalksDbContext walksDbContext)
        {
            this.walksDbContext = walksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)

        {
            // assign new id
            walk.Id = Guid.NewGuid();
            await walksDbContext.Walks.AddAsync(walk);
            await walksDbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await walksDbContext.Walks.FindAsync(id); //FirstOrDefaultAsync is diffferent so we have to use lamda
            if (existingWalk == null)
                return null;
            walksDbContext.Walks.Remove(existingWalk);
            await walksDbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalkAsync()
        {
            return await walksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public Task<Walk> GetAsync(Guid id)
        {
            return walksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpadteAsync(Guid id, Walk walk)
        {
            var existingWalk = await walksDbContext.Walks.FindAsync(id);
            if (existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                await walksDbContext.SaveChangesAsync();
                return existingWalk;
            }
            return null;
        }

        public Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            throw new NotImplementedException();
        }
    }
}
