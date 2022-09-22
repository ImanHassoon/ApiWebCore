using Microsoft.IdentityModel.Tokens;
using Webcore.API.Models.Domain;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Webcore.API.Repositories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
           
            // create Climate
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.Firstname));
            claims.Add(new Claim(ClaimTypes.Surname, user.Lastname));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            // loop into roles of users
            user.Roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            });
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials=new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials);
           return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token)); // this return a token based on the user information above
                

        }
    }
}
