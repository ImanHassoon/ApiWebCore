using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
