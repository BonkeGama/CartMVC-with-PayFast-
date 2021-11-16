using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartMvc.Models
{
    public class Cart_Item
    {
        [Key]
        public string cart_item_id { get; set; }
        [ForeignKey("Cart")]
        public string cart_id { get; set; }
        [ForeignKey("Item")]
        public int item_id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }

        public virtual Item Item { get; set; }
        public virtual Cart Cart { get; set; }
    }
}