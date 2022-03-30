using AutoMapper;
using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Entity;

namespace DotNet_Sample.Controllers.Mapper
{
    public class CategoryMapperProfile : Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<ECategory, Category>();
            CreateMap<Category, ECategory>();
        }
    }
}
