namespace BackendProyectoFinal.DTOs.Payment
{
    public class PaymentInsertDTO
    {
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public int OrderId { get; set; }
    }
}
