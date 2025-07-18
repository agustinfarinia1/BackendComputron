using BackendProyectoFinal.DTOs.ItemCart;

namespace BackendProyectoFinal.Services
{
    public interface IItemCartService: ICommonService<ItemCartDTO, ItemCartInsertDTO, ItemCartUpdateDTO>
    {
        public Task<IEnumerable<ItemCartDTO>?> GetItemCartByCartId(int ListID);
    }
}
