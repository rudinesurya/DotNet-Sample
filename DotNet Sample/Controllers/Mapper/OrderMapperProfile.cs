using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Entity;

namespace DotNet_Sample.Controllers.Mapper
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<EOrder, Order>();
        }
    }
}
