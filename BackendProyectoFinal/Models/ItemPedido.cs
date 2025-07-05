using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class ItemPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemPedidoID { get; set; }
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public virtual Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}
