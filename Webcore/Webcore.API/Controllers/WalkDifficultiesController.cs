using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webcore.API.Repositories;

namespace Webcore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(Repositories.IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkdiff = await walkDifficultyRepository.GetAllAsync();
            var walkdiffDto=mapper.Map<List<Models.DTO.WalkDifficulty>>(walkdiff);
           return Ok(walkdiffDto);
         
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultybyId")]
        public async Task<IActionResult> GetWalkDifficultybyId(Guid id)
        {
            var walkdiff=await walkDifficultyRepository.GetAsync(id);
            if (walkdiff == null)
                return NotFound();
            // convert domain object to DTO
            var walkdiffDto=mapper.Map<Models.DTO.WalkDifficulty>(walkdiff);
            return Ok(walkdiffDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyASync(Models.DTO.AddWalkDifficultyRequest walkDifficultyRequest)
        {
            var walkDiffDomain = new Models.Domain.WalkDifficulty
            {
                Code = walkDifficultyRequest.Code,
            };
            walkDiffDomain = await walkDifficultyRepository.AddAsync(walkDiffDomain);

       

            var walkDiffDto=mapper.Map<Models.DTO.WalkDifficulty>(walkDiffDomain);

            return CreatedAtAction(nameof(GetWalkDifficultybyId),new {id=walkDiffDto.Id},walkDiffDto);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public  async Task<IActionResult> UpdateWalkDiffAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDiffDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code,
            };
            if (walkDiffDomain==null)
                return NotFound();
            var walkDiffDto = mapper.Map<Models.DTO.WalkDifficulty>(walkDiffDomain);
            return Ok(walkDiffDto);

        }
    }
}
