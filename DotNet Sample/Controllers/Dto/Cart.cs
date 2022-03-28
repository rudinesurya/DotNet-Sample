namespace DotNet_Sample.Controllers.Dto
{
    public class Cart
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal TotalPrice { get; set; }
    }
}
