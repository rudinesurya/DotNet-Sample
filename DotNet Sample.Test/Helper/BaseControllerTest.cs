using AutoMapper;
using DotNet_Sample.Controllers.Mapper;

namespace DotNet_Sample.Test.Helper
{
    public class BaseControllerTest
    {
        protected readonly IMapper Mapper;

        public BaseControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(CartMapperProfile));
                cfg.AddProfile(typeof(CategoryMapperProfile));
                cfg.AddProfile(typeof(OrderMapperProfile));
                cfg.AddProfile(typeof(ProductMapperProfile));
            });

            Mapper = mockMapper.CreateMapper();
        }
    }
}
