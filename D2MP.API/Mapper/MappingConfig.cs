using AutoMapper;
using D2MP.API.DTO;
using D2MP.Models;

namespace D2MP.API.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<HeroDuo, DuoStats>();
        }
    }
}
