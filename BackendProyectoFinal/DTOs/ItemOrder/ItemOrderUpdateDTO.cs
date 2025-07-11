namespace BackendProyectoFinal.DTOs.ItemOrderDTO
{
    public class ItemOrderUpdateDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
