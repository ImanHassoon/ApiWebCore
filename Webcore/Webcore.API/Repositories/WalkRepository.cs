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

        }
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await walksDbContext.Walks.ToListAsync();
        }
    }
}
