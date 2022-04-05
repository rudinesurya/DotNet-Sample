using DotNet_Sample.Controllers.Dto;
using DotNet_Sample.Entity;
using System;

namespace DotNet_Sample.Test.MockData
{
    public class FixedData
    {
        /// <summary>
        /// DTO Fixed Data
        /// </summary>
        #region DTO

        public static Product GetNewProduct(Guid id, string name)
        {
            return new Product()
            {
                Id = id,
                Name = name,
                Summary = "Summary",
                Description = "Description",
                ImageFile = "default.png",
                Price = 1000.00M,
            };
        }

        #endregion

        /// <summary>
        /// Entity Fixed Data
        /// </summary>
        #region Entity

        public static ECategory GetNewECategory(Guid id, string name)
        {
            return new ECategory()
            {
                Id = id,
                Name = name,
                Description = "",
            };
        }

        public static EProduct GetNewEProduct(Guid id, string name)
        {
            return new EProduct()
            {
                Id = id,
                Name = name,
                Summary = "Summary",
                Description = "Description",
                ImageFile = "default.png",
                Price = 1000.00M,
            };
        }

        #endregion
    }
}
