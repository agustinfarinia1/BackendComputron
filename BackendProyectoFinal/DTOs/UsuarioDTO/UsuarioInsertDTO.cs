namespace BackendProyectoFinal.DTOs.UsuarioDTO
{
    public class UsuarioInsertDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public int RolID { get; set; }
        public int DomicilioID { get; set; }
        public bool Eliminado { get; set; }
    }
}
