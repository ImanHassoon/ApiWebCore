using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public interface IRegionRepository
    {
        Task <IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);
    }
}
