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
    }
}
