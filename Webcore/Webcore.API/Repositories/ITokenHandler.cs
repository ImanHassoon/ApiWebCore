using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public interface ITokenHandler
    {
       Task<string> CreateTokenAsync(User user);
    }
}
