namespace BackendProyectoFinal.DTOs.Payment.PaymentDetail
{
    public class PaymentDetailDTO
    {
        public int Id { get; set; }
        public string CardHolderName { get; set; }
        public string LastFourDigits { get; set; }
        public string CardType { get; set; }
        public string ExpirationDate { get; set; }
        public int PaymentId { get; set; }
    }
}
