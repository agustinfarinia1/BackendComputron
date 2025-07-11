using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class ItemCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemCartID { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public int CartID { get; set; }
        [ForeignKey("CartID")]
        public virtual Cart Cart { get; set; }
    }
}
