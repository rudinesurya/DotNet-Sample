namespace DotNet_Sample.Controllers.Dto.Cart_Action
{
    public class AddCartItem
    {
        public string UserName { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
