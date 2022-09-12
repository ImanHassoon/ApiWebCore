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
        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await walksDbContext.WalkDifficulties.ToListAsync();
           
        }
    }
}
