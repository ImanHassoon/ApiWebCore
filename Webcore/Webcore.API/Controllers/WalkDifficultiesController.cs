using Microsoft.AspNetCore.Mvc;
using Webcore.API.Repositories;

namespace Webcore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalkDifficultiesController(Repositories.IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
           return Ok(await walkDifficultyRepository.GetAllAsync());
         
        }
    }
}
