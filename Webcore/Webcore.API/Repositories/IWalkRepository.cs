using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public interface IWalkRepository  // add class -- interface
    {
        Task<IEnumerable<Walk>> GetAllWalkAsync();
        Task<Walk> GetAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpadteAsync(Guid id, Walk walk);
        Task<Walk> DeleteAsync(Guid id);
        
    }
}
