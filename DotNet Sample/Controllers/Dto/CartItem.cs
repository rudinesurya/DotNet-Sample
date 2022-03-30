namespace DotNet_Sample.Controllers.Dto
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Product? Product { get; set; }
    }
}
