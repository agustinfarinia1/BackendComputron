using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class TipoFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoFacturaID { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return "{ " + TipoFacturaID + " , " + Nombre + " }";
        }
    }
}
