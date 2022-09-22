using Microsoft.AspNetCore.Mvc;
using Webcore.API.Repositories;

namespace Webcore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller

    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> loginAsync(Models.DTO.LoginRequest loginRequest)
        {
            // check if user is authenticated


            var user=await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
            if (user !=null)
            {

                // generate a token
               var token=await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            //
            return BadRequest("Username or passwaord incorrect");
        }
    }
}
