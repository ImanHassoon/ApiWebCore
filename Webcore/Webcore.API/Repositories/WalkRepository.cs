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
            walk.Id=Guid.NewGuid(); 
            await walksDbContext.Walks.AddAsync(walk); 
            await walksDbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<IEnumerable<Walk>> GetAllWalkAsync()
        {
            return await walksDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public  Task<Walk> GetAsync(Guid id)
        {
            return  walksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
