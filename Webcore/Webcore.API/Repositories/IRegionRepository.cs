using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public interface IRegionRepository
    {
        Task <IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);
        Task<Region> AddAsync(Region region);   //return a region
        Task<Region> DeleteAsync(Guid id);
        Task<Region> UpdateAsync(Guid id, Region region);
    }
}
