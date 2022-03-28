using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet_Sample.Entity
{
    [Table("Catagories")]
    public class ECategory
    {
        public Guid Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
