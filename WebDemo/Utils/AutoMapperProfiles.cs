using AutoMapper;
using WebDemo.Common;
using WebDemo.Entity;

namespace WebDemo.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cliente, ClienteRequest>().ReverseMap();  
            CreateMap<Cliente, ClienteResponse>().ReverseMap();  
        }
    }
}
