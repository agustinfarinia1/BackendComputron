using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Cart;
using BackendProyectoFinal.DTOs.ItemCart;

namespace BackendProyectoFinal.Mappers
{
    public static class CartMapper
    {
        public static CartDTO ConvertCartToDTO(Cart carrito)
        {
            var cartDTO = new CartDTO()
            {
                Id = carrito.CartID,
                UserId = carrito.UserID
            };
            return cartDTO;
        }

        public static Cart ConvertDTOToModel(CartInsertDTO cartDTO)
        {
            var cart = new Cart()
            {
                UserID = cartDTO.UserId,
                ListCarts = new List<ItemCart>()
            };
            return cart;
        }

        public static void UpdateCart(CartDTO cartDTO,List<ItemCartDTO> listCart)
        {
            if (listCart.Count() > 0)
                cartDTO.ListCarts = listCart;
        }
    }
}
