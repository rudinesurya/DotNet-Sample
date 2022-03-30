using DotNet_Sample.Entity;

namespace DotNet_Sample.Controllers.Dto
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ImageFile { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
