using Webcore.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Webcore.API.Data;
using System.Linq;

namespace Webcore.API.Repositories
{
    public class UserRepository : IUserRepository
    {
      
        private readonly WalksDbContext WalksDbContext;

        public UserRepository(WalksDbContext walksDbContext)
        {
            this.WalksDbContext = walksDbContext;
         
        }

   

        public async Task<User> AuthenticateAsync(string username, string password)
        {

            var user = await WalksDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
            if (user == null)
            {

                return null;
            }
            var userRoles = await WalksDbContext.user_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach(var userrole in userRoles)
                {
                    var role = await WalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userrole.RoleId);
                    if (role!= null)
                    {
                        user.Roles.Add(role.Name);
                    }

                }
            }
            user.Password = null;
            return user;
        }
    }
}
