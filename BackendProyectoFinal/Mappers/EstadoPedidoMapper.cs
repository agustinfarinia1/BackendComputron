using BackendProyectoFinal.DTOs.EstadoPedidoDTO;
using BackendProyectoFinal.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackendProyectoFinal.Mappers
{
    public static class EstadoPedidoMapper
    {
        public static EstadoPedidoDTO ConvertirModelToDTO(EstadoPedido estadoPedido)
        {
            var estadoPedidoDTO = new EstadoPedidoDTO()
            {
                Id = estadoPedido.EstadoPedidoID,
                Nombre = estadoPedido.Nombre,
                EstadoSiguienteId = estadoPedido.EstadoSiguienteID
            };
            return estadoPedidoDTO;
        }

        public static EstadoPedido ConvertirDTOToModel(EstadoPedidoInsertDTO estadoPedidoDTO)
        {
            var estadoPedido = new EstadoPedido()
            {
                Nombre = estadoPedidoDTO.Nombre
            };
            if (estadoPedidoDTO.EstadoSiguienteId.HasValue) 
                estadoPedido.EstadoSiguienteID = estadoPedidoDTO.EstadoSiguienteId;

            return estadoPedido;
        }

        public static void ActualizarEstadoPedido(EstadoPedido estadoPedido, EstadoPedidoUpdateDTO estadoPedidoDTO)
        {
            if (!string.IsNullOrWhiteSpace(estadoPedidoDTO.Nombre))
                estadoPedido.Nombre = estadoPedidoDTO.Nombre;

            if (estadoPedidoDTO.EstadoSiguienteId.HasValue) 
                estadoPedido.EstadoSiguienteID = estadoPedidoDTO.EstadoSiguienteId;
        }

        public static EstadoPedidoUpdateDTO GenerarEstadoPedidoUpdateDTO(int id, string nombre,int? siguienteId)
        {
            var estadoPedidoUpdateDTO = new EstadoPedidoUpdateDTO()
            {
                Id = id,
                Nombre = nombre
            };
            if(siguienteId.HasValue) estadoPedidoUpdateDTO.EstadoSiguienteId = siguienteId;
            return estadoPedidoUpdateDTO;
        }
    }
}
