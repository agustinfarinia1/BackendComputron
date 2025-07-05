using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.DTOs.CarritoDTO
{
    public class CarritoInsertDTO
    {
        public int UsuarioId { get; set; }
        public List<ItemCarrito> ListaCarrito { get; set; }
    }
}
