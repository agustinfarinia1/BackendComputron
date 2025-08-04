using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Payment.PaymentMethod;

namespace BackendProyectoFinal.Mappers
{
    public static class PaymentMethodMapper
    {
        public static PaymentMethodDTO ConvertPaymentMethodToDTO(PaymentMethod paymentmethod)
        {
            var paymentMethodDTO = new PaymentMethodDTO()
            {
                Id = paymentmethod.PaymentMethodID,
                Name = paymentmethod.Name
            };
            return paymentMethodDTO;
        }
    }
}
