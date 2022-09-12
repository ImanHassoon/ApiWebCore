using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
    }
}
