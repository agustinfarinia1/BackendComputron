using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class EstadoPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstadoPedidoID { get; set; }
        public string Nombre { get; set; }
        public int? EstadoSiguienteID { get; set; }
        [ForeignKey("EstadoPedidoID")]
        public virtual EstadoPedido? EstadoSiguiente { get; set; }
    }
}
