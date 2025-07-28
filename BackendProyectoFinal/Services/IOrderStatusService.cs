using BackendProyectoFinal.DTOs.OrderStatus;

namespace BackendProyectoFinal.Services
{
    public interface IOrderStatusService : ICommonService<OrderStatusDTO,OrderStatusInsertDTO,OrderStatusUpdateDTO>
    {
        public Task<OrderStatusDTO?> GetFirstOrderStatus();
        public Task<OrderStatusDTO?> GetNextOrderStatus(int id);
        public Task<OrderStatusDTO?> GetLastOrderStatus();
    }
}
