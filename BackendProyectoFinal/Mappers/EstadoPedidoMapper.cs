using BackendProyectoFinal.DTOs.EstadoPedidoDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class EstadoPedidoMapper
    {
        public static EstadoPedidoDTO ConvertEstadoPedidoToDTO(EstadoPedido estadoPedido)
        {
            var estadoPedidoDTO = new EstadoPedidoDTO()
            {
                Id = estadoPedido.EstadoPedidoID,
                Nombre = estadoPedido.Nombre
            };
            return estadoPedidoDTO;
        }
    }
}
