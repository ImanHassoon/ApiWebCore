using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webcore.API.Repositories;

namespace Webcore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
           
        }

  

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();
          /*  var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
               var regionDTO = new Models.DTO.Region()
               {
                    Id = region.Id,
                   Code = region.Code,
                   Name = region.Name,
                   Area = region.Area,
                   Lat = region.Lat,
                   Long = region.Long,
                   Population = region.Population,
               };
                regionsDTO.Add(regionDTO);
                });*/
             var regionsDTO=mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);

            }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var regions = await regionRepository.GetAsync(id);
            if (regions == null)
            {
                return NotFound();
            }
            var regionsDTO=mapper.Map<Models.DTO.Region>(regions);
            return Ok(regionsDTO);

        }
    }
}
