using Webcore.API.Data;
using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalksDbContext walksDbContext;
        public WalkRepository(WalksDbContext walksDbContext)
        {

        }
        public Task<IEnumerable<Walk>> GetAllAsync()
        {
            walksDbContext.Walks.ToListAsync()
        }
    }
}
