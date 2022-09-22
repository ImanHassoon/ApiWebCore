using Webcore.API.Models.Domain;

namespace Webcore.API.Repositories
{
    public class staticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                Firstname ="ReadOnly",Lastname="User", Email ="ReadOnly@user.com",
                Id =Guid.NewGuid(),Username="ReadOnly@user.com", Password="ReadOnly@user.com",
                Roles=new List<string> {"Reader"}
            },
            new User()
            {
                Firstname ="Read Write",Lastname="User", Email ="ReadWrite@user.com",
                Id =Guid.NewGuid(),Username="ReadWrite@user.com", Password="Readwrite@user.com",
                Roles=new List<string> {"Reader", "Writer"}
            }

        };
        public async Task<User> AuthenticateAsync(string username, string password)
        {
           var user= Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);
           return user;
           
        }
    }
}
