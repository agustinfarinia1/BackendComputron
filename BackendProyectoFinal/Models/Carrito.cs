using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Carrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarritoID { get; set; }
        public int UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
        public List<ItemCarrito> ListaCarrito { get; set; }

        public Carrito()
        {
            ListaCarrito = new List<ItemCarrito>();
        }
    }
}
