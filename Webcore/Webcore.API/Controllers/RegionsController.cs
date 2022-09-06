using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webcore.API.Repositories;

namespace Webcore.API.Controllers
{
    [Route("api/[controller]")] //[controller] the name of controler variable which is value in this section is Region
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
        public async Task<IActionResult> GetAllRegions()
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
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var regions = await regionRepository.GetAsync(id);
            if (regions == null)
            {
                return NotFound();
            }
            var regionsDTO = mapper.Map<Models.DTO.Region>(regions);
            return Ok(regionsDTO);

        }
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //request(DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            //pass details to the repository
            region = await regionRepository.AddAsync(region);

            // Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)


        {
            // get region from db
           var region =await regionRepository.DeleteAsync(id);

            // if null
            if (region == null)
                return NotFound();

            // convert responce to dto
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };

            //return OK response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> PutRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateregion)
        {
            // convert Dto to domain model
            var region = new Models.Domain.Region()
            {
                Code = updateregion.Code,
                Area = updateregion.Area,
                Lat = updateregion.Lat,
                Long = updateregion.Long,
                Name = updateregion.Name,
                Population = updateregion.Population
            };
            // update region using repository
             region = await regionRepository.UpdateAsync(id,region);

            // if null ==> not found
            if (region == null)
                return null;

            // convert domain back to Dto
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population

            };

            // return OK 
            return Ok(regionDTO);
        }
    }
}
