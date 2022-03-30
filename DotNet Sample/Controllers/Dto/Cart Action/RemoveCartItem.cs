namespace DotNet_Sample.Controllers.Dto.Cart_Action
{
    public class RemoveCartItem
    {
        public Guid CartId { get; set; }

        public Guid CartItemId { get; set; }
    }
}
