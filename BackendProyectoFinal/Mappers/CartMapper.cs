using BackendProyectoFinal.DTOs.CartDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class CartMapper
    {
        public static CartDTO ConvertCartToDTO(Cart carrito)
        {
            var cartDTO = new CartDTO()
            {
                Id = carrito.CartID,
                UserId = carrito.UserID,
                ListCarts = carrito.ListCarts
            };
            return cartDTO;
        }

        public static Cart ConvertDTOToModel(CartInsertDTO cartDTO)
        {
            var cart = new Cart()
            {
                UserID = cartDTO.UserId,
                ListCarts = cartDTO.ListCarts
            };
            return cart;
        }

        public static void UpdateCart(Cart cart, CartUpdateDTO cartDTO)
        {
            if (cartDTO.Id > 0)
                cart.CartID = cartDTO.Id;

            if (cartDTO.UserId > 0)
                cart.UserID = cartDTO.UserId;

            if (cartDTO.ListCarts.Count() > 0)
            {
                cart.ListCarts = cartDTO.ListCarts;
            }
        }
    }
}
