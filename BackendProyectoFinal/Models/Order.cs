using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public List<ItemOrder> ListOrders { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public int StatusdOrderID { get; set; }
        [ForeignKey("StatusdOrderID")]
        public virtual OrderStatus StatusdOrder { get; set; }
        public int AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }

        public Order()
        {
            ListOrders = new List<ItemOrder>();
        }
    }
}
