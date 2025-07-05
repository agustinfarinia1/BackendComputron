using BackendProyectoFinal.DTOs.CarritoDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class CarritoMapper
    {
        public static CarritoDTO ConvertCarritoToDTO(Carrito carrito)
        {
            var carritoDTO = new CarritoDTO()
            {
                Id = carrito.CarritoID,
                UsuarioId = carrito.UsuarioID,
                ListaCarrito = carrito.ListaCarrito
            };
            return carritoDTO;
        }

        public static Carrito ConvertDTOToModel(CarritoInsertDTO carritoDTO)
        {
            var carrito = new Carrito()
            {
                UsuarioID = carritoDTO.UsuarioId,
                ListaCarrito = carritoDTO.ListaCarrito
            };
            return carrito;
        }

        public static void ActualizarCarrito(Carrito carrito, CarritoUpdateDTO carritoDTO)
        {
            if (carritoDTO.Id > 0)
                carrito.CarritoID = carritoDTO.Id;

            if (carritoDTO.UsuarioId > 0)
                carrito.UsuarioID = carritoDTO.UsuarioId;

            if (carritoDTO.ListaCarrito.Count() > 0)
            {
                carrito.ListaCarrito = carritoDTO.ListaCarrito;
            }
        }
    }
}
