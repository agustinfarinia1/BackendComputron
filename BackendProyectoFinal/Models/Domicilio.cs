using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Domicilio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DomicilioID {  get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int? Piso {  get; set; }
        public string? Departamento { get; set; }
    }
}
