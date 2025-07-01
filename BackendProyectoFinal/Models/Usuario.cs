using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public int RolID { get; set; }
        [ForeignKey("RolID")]
        public virtual Rol Rol { get; set; }
        public int DomicilioID { get; set; }
        [ForeignKey("DomicilioID")]
        public virtual Domicilio Domicilio { get; set; }
        public bool Eliminado { get; set; }
    }
}
