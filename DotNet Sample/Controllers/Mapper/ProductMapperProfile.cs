using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Entity;

namespace DotNet_Sample.Controllers.Mapper
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<EProduct, Product>();
            CreateMap<Product, EProduct>().ForMember(x => x.Category, options => options.Ignore());
        }
    }
}
