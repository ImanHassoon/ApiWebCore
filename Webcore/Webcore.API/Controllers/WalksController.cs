using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webcore.API.Repositories;
namespace Webcore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController
    {
       
       
            private readonly IWalkRepository walkRepository;
            private readonly IMapper mapper;

            public WalksController(IRegionRepository regionRepository, IMapper mapper)
            {
                this.walkRepository = walkRepository;
                this.mapper = mapper;

            }
        [HttpGet]
        public async Task<IActionResult> GetAllWalk()
        {
            var walk=new 
        }

        }
    }
