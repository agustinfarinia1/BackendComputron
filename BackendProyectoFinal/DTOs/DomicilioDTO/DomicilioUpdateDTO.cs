namespace BackendProyectoFinal.DTOs
{
    public class DomicilioUpdateDTO
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int? Piso {  get; set; }
        public string? Departamento { get; set; }
    }
}
