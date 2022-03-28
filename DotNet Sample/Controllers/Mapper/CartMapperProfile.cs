using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Entity;

namespace DotNet_Sample.Controllers.Mapper
{
    public class CartMapperProfile : Profile
    {
        public CartMapperProfile()
        {
            CreateMap<ECart, Cart>();
            CreateMap<ECartItem, CartItem>();
        }
    }
}
