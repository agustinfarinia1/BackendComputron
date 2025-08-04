using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Cart.ItemCart;

namespace BackendProyectoFinal.Mappers
{
    public static class ItemCartMapper
    {
        public static ItemCartDTO ConvertItemCartToDTO(ItemCart itemCart)
        {
            var itemCartDTO = new ItemCartDTO()
            {
                Id = itemCart.ItemCartID,
                Quantity = itemCart.Quantity,
                CartId = itemCart.CartID,
                ProductId = itemCart.ProductID
            };
            return itemCartDTO;
        }

        public static ItemCart ConvertDTOToModel(ItemCartInsertDTO itemCartDTO)
        {
            var itemCart = new ItemCart()
            {
                CartID = itemCartDTO.CartId,
                ProductID = itemCartDTO.ProductId,
                Quantity = itemCartDTO.Quantity
            };
            return itemCart;
        }

        public static void UpdateItemCart(ItemCart itemCart, ItemCartUpdateDTO itemCartDTO)
        {
            if (itemCartDTO.ProductId > 0)
                itemCart.ProductID = itemCartDTO.ProductId;

            if (itemCartDTO.CartId > 0)
            {
                itemCart.CartID = itemCartDTO.CartId;
            }

            if (itemCartDTO.Quantity > 0)
            {
                itemCart.Quantity = itemCartDTO.Quantity;
            }
        }
    }
}
