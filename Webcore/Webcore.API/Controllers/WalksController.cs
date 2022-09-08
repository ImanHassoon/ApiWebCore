using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webcore.API.Repositories;
namespace Webcore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController: Controller // do not forget to create profile
    {
       
       
            private readonly IWalkRepository walkRepository;
            private IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
            {
                this.walkRepository = walkRepository;
                this.mapper = mapper;

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
        [ActionName("AddWalkAsync")]
       
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
            //Convert DTO to Domain 
            var walkDoamin = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            //pass domain object to repository to presist this
            var walkdmn=await walkRepository.AddAsync(walkDoamin);

            //convert the domain object into dto
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDoamin.Id,
                Name = walkDoamin.Name,
                RegionId = walkDoamin.RegionId,
                WalkDifficultyId = walkDoamin.WalkDifficultyId,
            };

            //send dto back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id, walkDTO });

        }

    }
    }
