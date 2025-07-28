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
        public DateOnly CreationDate { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public int OrderStatusID { get; set; }
        [ForeignKey("OrderStatusID")]
        public virtual OrderStatus OrderStatus { get; set; }
        public int AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
        public bool Canceled { get; set; }

        public Order()
        {
            ListOrders = new List<ItemOrder>();
        }
    }
}
