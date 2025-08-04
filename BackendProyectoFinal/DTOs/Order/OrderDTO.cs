using BackendProyectoFinal.DTOs.Order.ItemOrder;

namespace BackendProyectoFinal.DTOs.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public List<ItemOrderDTO> ListOrders { get; set; }
        public DateOnly CreationDate { get; set; }
        public int UserId { get; set; }
        public int OrderStatusId { get; set; }
        public int AddressId { get; set; }
        public bool Canceled { get; set; }
    }
}
