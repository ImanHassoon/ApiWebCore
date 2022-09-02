using Microsoft.EntityFrameworkCore;
using Webcore.API.Data;
using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalksDbContext walksDbContext;

        public RegionRepository(WalksDbContext walksDbContext)
        {
            this.walksDbContext = walksDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await  walksDbContext.Regions.ToListAsync();
        }
    }
}
