using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkdiff = await walkDifficultyRepository.GetAllAsync();
            var walkdiffDto=mapper.Map<List<Models.DTO.WalkDifficulty>>(walkdiff);
           return Ok(walkdiffDto);
         
        }
        
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultybyId")]
        [Authorize(Roles = "Reader")]
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
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddWalkDifficultyASync(Models.DTO.AddWalkDifficultyRequest walkDifficultyRequest)
        {
            // validate

           // if (!(ValidateAddWalkDiff(walkDifficultyRequest)))
            //{
              //  return BadRequest(ModelState);
            //}
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
        [Authorize(Roles = "Writer")]
        public  async Task<IActionResult> UpdateWalkDiffAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // validate
            if (!ValidateUpdateWalkDiff(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }
            //  convert
            var walkDiffDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code,
            };
            if (walkDiffDomain==null)
                return NotFound();
            var walkDiffDto = mapper.Map<Models.DTO.WalkDifficulty>(walkDiffDomain);
            return Ok(walkDiffDto);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteWalkDiffAsync(Guid id)
        {
            var walkdiffdele=await walkDifficultyRepository.DeleteAsync(id);
            if (walkdiffdele == null)
                return null;
         
            var   walkDiffDto=mapper.Map<Models.DTO.WalkDifficulty>(walkdiffdele);
            return Ok(walkDiffDto);

        }
        #region private methods
        private bool ValidateAddWalkDiff(Models.DTO.AddWalkDifficultyRequest walkDifficultyRequest)
        {
            if (walkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(walkDifficultyRequest), $"{nameof(walkDifficultyRequest)} can not not be null");
                return false;
            }
            if(string.IsNullOrEmpty(walkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(walkDifficultyRequest.Code), $"{nameof(walkDifficultyRequest.Code)} can not not be null");
                return false;
            }
            return true;
        }

        private bool ValidateUpdateWalkDiff(Models.DTO.UpdateWalkDifficultyRequest walkDifficultyUpdateRequest)
        {
            if (walkDifficultyUpdateRequest == null)
            {
                ModelState.AddModelError(nameof(walkDifficultyUpdateRequest), $"{nameof(walkDifficultyUpdateRequest)} can not not be null");
                return false;
            }
            if (string.IsNullOrEmpty(walkDifficultyUpdateRequest.Code))
            {
                ModelState.AddModelError(nameof(walkDifficultyUpdateRequest.Code), $"{nameof(walkDifficultyUpdateRequest.Code)} can not not be null");
                return false;
            }
            return true;
        }
        #endregion
    }
}
