namespace BackendProyectoFinal.DTOs.Payment
{
    public class PaymentUpdateDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaidAt { get; set; }
        public int PaymentMethodId { get; set; }
        public int OrderId { get; set; }
    }
}
