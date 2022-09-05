using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public interface IWalkRepository  // add class -- interface
    {
        Task<IEnumerable<Walk>> GetAllAsync()
    }
}
