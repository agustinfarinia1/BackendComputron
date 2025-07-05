using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.DTOs.PedidoDTO
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public List<ItemPedido> ListaPedido { get; set; }
        public int UsuarioId { get; set; }
        public int EstadoPedidoID { get; set; }
        public int DomicilioId { get; set; }
    }
}
