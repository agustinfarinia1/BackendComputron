using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.DTOs.CarritoDTO
{
    public class CarritoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public List<ItemCarrito> ListaCarrito { get; set; }
    }
}
