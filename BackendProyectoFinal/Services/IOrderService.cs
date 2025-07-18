using BackendProyectoFinal.DTOs.Order;

namespace BackendProyectoFinal.Services
{
    public interface IOrderService : ICommonService<OrderDTO, OrderInsertDTO, OrderUpdateDTO>
    {
        public void AddItemOrder(int orderID, int userID);
        public void UpdateItemOrder(int orderID, int userID);
        public void RemoveItemOrder(int orderID, int userID);
    }
}
