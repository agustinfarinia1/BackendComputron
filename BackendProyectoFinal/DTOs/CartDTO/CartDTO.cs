using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.DTOs.CartDTO
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<ItemCart> ListCarts { get; set; }
    }
}
