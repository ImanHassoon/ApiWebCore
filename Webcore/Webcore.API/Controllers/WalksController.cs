using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webcore.API.Repositories;
namespace Webcore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller // do not forget to create profile
    {


        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalk()
        {

            //fetch data from database-domain
            var walks = await walkRepository.GetAllWalkAsync();

            // convert data from domain to dto
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks); //List because the return parameter is a list of walks
            // return response

            return Ok(walksDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetWalkAsync")]

        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walks = await walkRepository.GetAsync(id);
            // convert data from domain to dto
            var walksDTO = mapper.Map<Models.DTO.Walk>(walks); // without list because the parameter is single walk
            // return response

            return Ok(walksDTO);

        }

        [HttpPost]

        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)

        {

            if (!(await ValidateAddWalkAsync(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }
            //Convert DTO to Domain 
            var walkDoamin = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            //pass domain object to repository to presist this
            var walkdmn = await walkRepository.AddAsync(walkDoamin);

            //convert the domain object into dto
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDoamin.Id,
                Name = walkDoamin.Name,
                Length = walkDoamin.Length,
                RegionId = walkDoamin.RegionId,
                WalkDifficultyId = walkDoamin.WalkDifficultyId,
            };

            //send dto back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // Validate
            if (!(await ValidateUpdateWalkAsync(updateWalkRequest)))
            {
                return BadRequest(ModelState);
            }
            //convert DTO to domain
            var walkDoamin = new Models.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };


            // pass details to repository 
            walkDoamin = await walkRepository.UpadteAsync(id, walkDoamin);

            // handle Null
            if (walkDoamin == null)
            {
                return null;
            }
            //convert back domain to dto

            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDoamin.Id,
                Name = walkDoamin.Name,
                RegionId = walkDoamin.RegionId,
                WalkDifficultyId = walkDoamin.WalkDifficultyId,
            };
            // return response    
            return Ok(walkDTO);




        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleyeWalkAsync(Guid id)
        {
            // get walk from db
            var walkDomain = await walkRepository.DeleteAsync(id);

            // if null
            if (walkDomain == null)
                return NotFound();

            // convert responce to dto
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            //return OK response
            return Ok(walkDTO);
        }



        #region private methods
        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            if (addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest), $"{nameof(addWalkRequest)} can not not be null");
                return false;
            }
            if (string.IsNullOrEmpty(addWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(addWalkRequest.Name), $"{nameof(addWalkRequest.Name)} can not not be empty");
            }
            if (addWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest.Length), $"{nameof(addWalkRequest.Length)} can not not be <= 0");
            }
            var region = await regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId), $"{nameof(addWalkRequest.RegionId)} is not valid");
            }
            var walkDiff = await walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);
            if (walkDiff == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId), $"{nameof(addWalkRequest.WalkDifficultyId)} is not valid");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        private async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest), $"{nameof(updateWalkRequest)} can not not be null");
                return false;
            }
            if (string.IsNullOrEmpty(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Name), $"{nameof(updateWalkRequest.Name)} can not not be empty");
            }
            if (updateWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length), $"{nameof(updateWalkRequest.Length)} can not not be <= 0");
            }
            var region = await regionRepository.GetAsync(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId), $"{nameof(updateWalkRequest.RegionId)} is not valid");
            }
            var walkDiff = await walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (walkDiff == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId), $"{nameof(updateWalkRequest.WalkDifficultyId)} is not valid");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion


    }
}