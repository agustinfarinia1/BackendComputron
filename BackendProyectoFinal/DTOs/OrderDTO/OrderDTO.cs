using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.DTOs.OrderDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public List<ItemOrder> ListOrders { get; set; }
        public int UserId { get; set; }
        public int OrderStatusId { get; set; }
        public int AddressId { get; set; }
    }
}
