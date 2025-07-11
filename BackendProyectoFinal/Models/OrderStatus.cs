using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class OrderStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderStatusID { get; set; }
        public string Name { get; set; }
        public int? NextStatusOrderID { get; set; }
        [ForeignKey("NextStatusOrderID")]
        public virtual OrderStatus? NextStatus { get; set; }
    }
}
