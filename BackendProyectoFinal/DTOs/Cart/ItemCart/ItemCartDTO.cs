namespace BackendProyectoFinal.DTOs.Cart.ItemCart
{
    public class ItemCartDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }

        public static implicit operator List<object>(ItemCartDTO? v)
        {
            throw new NotImplementedException();
        }
    }
}
