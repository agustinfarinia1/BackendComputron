namespace BackendProyectoFinal.DTOs.OrderStatusDTO
{
    public class OrderStatusUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? NextStatusOrderId { get; set; }
    }
}
