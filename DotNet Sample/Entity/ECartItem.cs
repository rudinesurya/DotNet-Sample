using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet_Sample.Entity
{
    [Table("Cart_Items")]
    public class ECartItem
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Guid ProductId { get; set; }

        public EProduct? Product { get; set; }
    }
}
