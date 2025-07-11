using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.DTOs.CartDTO
{
    public class CartInsertDTO
    {
        public int UserId { get; set; }
        public List<ItemCart> ListCarts { get; set; }
    }
}
