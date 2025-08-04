using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaidAt { get; set; }
        public int PaymentMethodID { get; set; }
        [ForeignKey("PaymentMethodID")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
