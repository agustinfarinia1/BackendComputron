namespace BackendProyectoFinal.DTOs.Order.ItemOrder
{
    public class ItemOrderDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
