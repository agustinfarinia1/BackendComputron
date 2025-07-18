using BackendProyectoFinal.DTOs.Cart;
using BackendProyectoFinal.DTOs.ItemCart;

namespace BackendProyectoFinal.Services
{
    public interface ICartService : ICommonService<CartDTO, CartInsertDTO, CartUpdateDTO>
    {
        public void EmptyCart(int cartID,int userID);
    }
}
