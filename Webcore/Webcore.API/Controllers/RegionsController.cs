using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;

        }



        [HttpGet]
        [Authorize(Roles = "Reader")]
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
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);

        }
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetRegionAsync")]
        [Authorize(Roles = "Reader")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {

            // validate the request
            //if (!ValidateAddRegionAsync(addRegionRequest))
           // {
             //   return BadRequest(ModelState);
           // }
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
        [Authorize]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)


        {
            // get region from db
            var region = await regionRepository.DeleteAsync(id);

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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> PutRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateregion)
        {
            // validate the request
           // if (!ValidateUpdateRegionAsync(updateregion))
           // {
            //    return BadRequest(ModelState);
           // }
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
            region = await regionRepository.UpdateAsync(id, region);

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
        #region MyRegion
        private Boolean ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $"{nameof(addRegionRequest)} can not not be null");
                return false;
            }
            if (string.IsNullOrEmpty(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), $"{nameof(addRegionRequest.Code)} can not not be empty");
            }

            if (string.IsNullOrEmpty(addRegionRequest.Name))
                ModelState.AddModelError(nameof(addRegionRequest.Name), $"{nameof(addRegionRequest.Name)} can not not be empty");



            if (addRegionRequest.Area <= 0)
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} can not not be less or equal to 0");
            if (addRegionRequest.Lat <= 0)
                ModelState.AddModelError(nameof(addRegionRequest.Lat), $"{nameof(addRegionRequest.Lat)} can not not be less or equal to 0");

            if (addRegionRequest.Long <= 0)
                ModelState.AddModelError(nameof(addRegionRequest.Long), $"{nameof(addRegionRequest.Long)} can not not be less or equal to 0");
            if (addRegionRequest.Population < 0)
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{nameof(addRegionRequest.Population)} can not not be less or equal to 0");
            if (ModelState.ErrorCount > 0)
            {

                return false;
            }
            return true;
        }

        private Boolean ValidateUpdateRegionAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest), $"{nameof(updateRegionRequest)} can not not be null");
                return false;
            }
            if (string.IsNullOrEmpty(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code), $"{nameof(updateRegionRequest.Code)} can not not be empty");
            }

            if (string.IsNullOrEmpty(updateRegionRequest.Name))
                ModelState.AddModelError(nameof(updateRegionRequest.Name), $"{nameof(updateRegionRequest.Name)} can not not be empty");



            if (updateRegionRequest.Area <= 0)
                ModelState.AddModelError(nameof(updateRegionRequest.Area), $"{nameof(updateRegionRequest.Area)} can not not be less or equal to 0");
            if (updateRegionRequest.Lat <= 0)
                ModelState.AddModelError(nameof(updateRegionRequest.Lat), $"{nameof(updateRegionRequest.Lat)} can not not be less or equal to 0");

            if (updateRegionRequest.Long <= 0)
                ModelState.AddModelError(nameof(updateRegionRequest.Long), $"{nameof(updateRegionRequest.Long)} can not not be less or equal to 0");
            if (updateRegionRequest.Population < 0)
                ModelState.AddModelError(nameof(updateRegionRequest.Population), $"{nameof(updateRegionRequest.Population)} can not not be less or equal to 0");
            if (ModelState.ErrorCount > 0)
            {

                return false;
            }
            return true;
        }
        #endregion
    }
}