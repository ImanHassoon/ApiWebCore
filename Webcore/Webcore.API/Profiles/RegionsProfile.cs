using AutoMapper;
using Webcore.API.Models;

namespace Webcore.API.Profiles

{
    public class RegionsProfile : Profile // : profile مهمة 
{
    public RegionsProfile()
    {
        CreateMap<Models.Domain.Region, Models.DTO.Region>()
            .ReverseMap();// بالاتجاهين
    }
}
}