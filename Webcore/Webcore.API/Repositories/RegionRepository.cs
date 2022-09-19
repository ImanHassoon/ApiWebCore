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
        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await walksDbContext.AddAsync(region);
            await walksDbContext.SaveChangesAsync();
            return region;
        }



        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await walksDbContext.Regions.ToListAsync();
        }
        public async Task<Region> GetAsync(Guid id)
        {
            return await walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existregion = await walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existregion == null)
                return null;
            existregion.Code = region.Code;
            existregion.Name = region.Name;
            existregion.Area = region.Area;
            existregion.Lat = region.Lat;
            existregion.Long = region.Long;
            existregion.Population = region.Population;

            await walksDbContext.SaveChangesAsync();
            return existregion;

        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
                return null;
            walksDbContext.Regions.Remove(region);
            await walksDbContext.SaveChangesAsync();
            return region;
        }
        public async Task<Region> GetAsync(Guid id)
        {
            return await walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existregion=await walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existregion == null)
                return null;
            existregion.Code=region.Code;
            existregion.Name=region.Name;
            existregion.Area=region.Area;
            existregion.Lat=region.Lat;
            existregion.Long=region.Long;
            existregion.Population=region.Population;

            await walksDbContext.SaveChangesAsync();
            return existregion; 

        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
                return null;
            walksDbContext.Regions.Remove(region);
            await walksDbContext.SaveChangesAsync();
            return region;
        }
    }
}