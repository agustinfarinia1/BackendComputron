using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PedidoID { get; set; }
        public List<ItemPedido> ListaPedido { get; set; }
        public int UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
        public int EstadoPedidoID { get; set; }
        [ForeignKey("EstadoPedidoID")]
        public virtual EstadoPedido EstadoPedido { get; set; }
        public int DomicilioID { get; set; }
        [ForeignKey("DomicilioID")]
        public virtual Domicilio DomicilioEntrega { get; set; }

        public Pedido()
        {
            ListaPedido = new List<ItemPedido>();
        }
    }
}
