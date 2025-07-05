using BackendProyectoFinal.DTOs.PedidoDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class PedidoMapper
    {
        public static PedidoDTO ConvertPedidoToDTO(Pedido pedido)
        {
            var pedidoDTO = new PedidoDTO()
            {
                Id = pedido.PedidoID,
                UsuarioId = pedido.UsuarioID,
                ListaPedido = pedido.ListaPedido,
                DomicilioId = pedido.DomicilioID
            };
            return pedidoDTO;
        }

        public static Pedido ConvertDTOToModel(PedidoInsertDTO pedidoDTO)
        {
            var pedido = new Pedido()
            {
                UsuarioID = pedidoDTO.UsuarioId,
                ListaPedido = pedidoDTO.ListaPedido,
                DomicilioID = pedidoDTO.DomicilioId,
            };
            return pedido;
        }

        public static void ActualizarPedido(Pedido pedido, PedidoUpdateDTO pedidoDTO)
        {
            if (pedidoDTO.Id > 0)
                pedido.PedidoID = pedidoDTO.Id;

            if (pedidoDTO.UsuarioId > 0)
                pedido.UsuarioID = pedidoDTO.UsuarioId;

            if (pedidoDTO.ListaPedido.Count() > 0)
            {
                pedido.ListaPedido = pedidoDTO.ListaPedido;
            }
            if (pedidoDTO.DomicilioId > 0)
            {
                pedido.DomicilioID = pedidoDTO.DomicilioId;
            }
        }
    }
}
