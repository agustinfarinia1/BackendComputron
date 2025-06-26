using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class CategoriaProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaProductoID {  get; set; }
        public string Nombre {  get; set; }

        public override string ToString()
        {
            return "{ " + CategoriaProductoID + Nombre + " }";
        }
    }
}
