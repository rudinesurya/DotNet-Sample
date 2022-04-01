using DotNet_Sample.Entity;
using System;
using System.Collections.Generic;

namespace DotNet_Sample.Test.MockData
{
    internal class FixedData
    {
        public static IEnumerable<ECategory> GetFixedCategories()
        {
            return new List<ECategory>()
            {
                new ECategory()
                {
                    Id = Guid.Parse("64a7388c-65e9-42e1-bab1-39cd105f8675"),
                    Name = "White",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                },
                new ECategory()
                {
                    Id = Guid.Parse("c921ef76-0af6-4ac9-a7fd-864b4898d60b"),
                    Name = "Black",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat.",
                },
            };
        }

        public static IEnumerable<EProduct> GetFixedProducts()
        {
            return new List<EProduct>()
            {
                new EProduct()
                {
                    Id = Guid.Parse("2cff423d-0852-4406-ac3a-32a39a0253c0"),
                    Name = "IPhone X",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    CategoryId = Guid.Parse("64a7388c-65e9-42e1-bab1-39cd105f8675")
                },
                new EProduct()
                {
                    Id = Guid.Parse("c8e86dc0-17ab-4b0e-8205-1c29afe50c09"),
                    Name = "Samsung 10",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                    ImageFile = "product-2.png",
                    Price = 840.00M,
                    CategoryId = Guid.Parse("c921ef76-0af6-4ac9-a7fd-864b4898d60b")
                },
            };
        }

        public static EProduct GetNewProduct()
        {
            return new EProduct()
            {
                Id = Guid.Parse("f17d0c63-82fe-417c-afc1-5a8dc51886d1"),
                Name = "Samsung 20",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-2.png",
                Price = 840.00M,
                CategoryId = Guid.Parse("c921ef76-0af6-4ac9-a7fd-864b4898d60b")
            };
        }
    }
}
