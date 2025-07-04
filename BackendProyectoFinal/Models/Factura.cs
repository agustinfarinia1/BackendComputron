using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacturaID {  get; set; }
        public DateTime Fecha {  get; set; }
        public int TipoFacturaID {  get; set; }
        [ForeignKey("TipoFacturaID")]
        public virtual TipoFactura TipoFactura { get; set; }
        public string RazonSocial {  get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Importe {  get; set; }

        public override string ToString()
        {
            return "{ " + FacturaID + " , " + Fecha + " , " + RazonSocial + " , " + Importe + " }";
        }
    }
}
