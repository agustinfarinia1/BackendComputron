using BackendProyectoFinal.DTOs.OrderStatus;

namespace BackendProyectoFinal.Services
{
    public interface IOrderStatusService : ICommonService<OrderStatusDTO,OrderStatusInsertDTO,OrderStatusUpdateDTO>
    {
        public Task<OrderStatusDTO?> GetFirst();
        public Task<OrderStatusDTO?> GetLast();
    }
}
