using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoID { get; set; }
        public string CodigoML { get; set; }
        public string Titulo {  get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }
        public int CategoriaProductoID { get; set; }
        [ForeignKey("CategoriaProductoID")]
        public virtual CategoriaProducto CategoriaProducto { get; set; }
        public bool Eliminado { get; set; }
    }
}
