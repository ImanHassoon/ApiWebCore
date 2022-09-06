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
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walks);
            // return response

            return Ok(walksDTO);
        }

        }
    }
