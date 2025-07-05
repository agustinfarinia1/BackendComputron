using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.DTOs.PedidoDTO
{
    public class PedidoInsertDTO
    {
        public List<ItemPedido> ListaPedido { get; set; }
        public int UsuarioId { get; set; }
        public int EstadoPedidoID { get; set; }
        public int DomicilioId { get; set; }
    }
}
