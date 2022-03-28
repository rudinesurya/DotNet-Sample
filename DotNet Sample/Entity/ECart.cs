using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet_Sample.Entity
{
    [Table("Carts")]
    public class ECart
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public List<ECartItem> Items { get; set; } = new List<ECartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }

                return totalprice;
            }
        }
    }
}
