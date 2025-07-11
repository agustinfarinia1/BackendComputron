using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.DTOs.CartDTO
{
    public class CartUpdateDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<ItemCart> ListCarts { get; set; }
    }
}
