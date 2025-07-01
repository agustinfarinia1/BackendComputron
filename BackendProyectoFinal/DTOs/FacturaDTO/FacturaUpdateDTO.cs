namespace BackendProyectoFinal.DTOs
{
    public class FacturaUpdateDTO
    {
        public int Id {  get; set; }
        public DateTime Fecha {  get; set; }
        public int TipoFactura {  get; set; }
        public string RazonSocial {  get; set; }
        public decimal importe {  get; set; }
    }
}
