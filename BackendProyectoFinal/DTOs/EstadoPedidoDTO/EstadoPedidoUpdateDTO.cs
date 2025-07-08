namespace BackendProyectoFinal.DTOs.EstadoPedidoDTO
{
    public class EstadoPedidoUpdateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? EstadoSiguienteId { get; set; }
    }
}
