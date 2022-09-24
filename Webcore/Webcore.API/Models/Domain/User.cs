using System.ComponentModel.DataAnnotations.Schema;

namespace Webcore.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
      
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }

        //navigation prop
        public List<User_Role> UserRoles { get; set; }

    }
}
