using BackendProyectoFinal.DTOs.ItemOrder;

namespace BackendProyectoFinal.DTOs.Order
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public List<ItemOrderDTO> ListOrders { get; set; }
        public int UserId { get; set; }
        public int OrderStatusId { get; set; }
        public int AddressId { get; set; }
    }
}
