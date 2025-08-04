using BackendProyectoFinal.DTOs.Cart;
using BackendProyectoFinal.DTOs.Cart.ItemCart;
using BackendProyectoFinal.DTOs.Payment;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class PaymentMapper
    {
        public static PaymentDTO ConvertPaymentToDTO(Payment payment)
        {
            var paymentDTO = new PaymentDTO()
            {
                Id = payment.PaymentID,
                Amount = payment.Amount,
                PaidAt = payment.PaidAt,
                PaymentMethodId = payment.PaymentMethodID
            };
            return paymentDTO;
        }

        public static Payment ConvertDTOToModel(PaymentInsertDTO paymentDTO)
        {
            var payment = new Payment()
            {
                Amount = paymentDTO.Amount,
                PaidAt= DateOnly.FromDateTime(DateTime.Now),
                PaymentMethodID = paymentDTO.PaymentMethodId,
                OrderID = paymentDTO.OrderId
            };
            return payment;
        }

        public static void UpdatePayment(Payment payment,PaymentUpdateDTO paymentDTO)
        {
            if (paymentDTO.Amount > 0)
                payment.Amount = paymentDTO.Amount;

            if (paymentDTO.PaidAt != payment.PaidAt)
                payment.PaidAt = paymentDTO.PaidAt;
        }
    }
}
