using BackendProyectoFinal.DTOs.Payment.PaymentDetail;
using BackendProyectoFinal.DTOs.Product;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class PaymentDetailMapper
    {
        public static PaymentDetailDTO ConvertPaymentDetailToDTO(PaymentDetail paymentDetail)
        {
            var paymentDetailDTO = new PaymentDetailDTO()
            {
                Id = paymentDetail.PaymentDetailID,
                CardHolderName = paymentDetail.CardHolderName,
                LastFourDigits = paymentDetail.LastFourDigits,
                CardType = paymentDetail.CardType,
                ExpirationDate = paymentDetail.ExpirationDate,
                PaymentId = paymentDetail.PaymentID
            };
            return paymentDetailDTO;
        }

        public static PaymentDetail ConvertDTOToModel(PaymentDetailInsertDTO paymentDetailDTO)
        {
            var payment = new PaymentDetail()
            {
                CardHolderName = paymentDetailDTO.CardHolderName,
                LastFourDigits = paymentDetailDTO.LastFourDigits,
                CardType = paymentDetailDTO.CardType,
                ExpirationDate = paymentDetailDTO.ExpirationDate,
                PaymentID = paymentDetailDTO.PaymentId
            };
            return payment;
        }

        public static void UpdatePaymentDetail(PaymentDetail paymentDetail, PaymentDetailUpdateDTO paymentDetailDTO)
        {
            if (!string.IsNullOrWhiteSpace(paymentDetailDTO.CardHolderName))
                paymentDetail.CardHolderName = paymentDetailDTO.CardHolderName;

            if (!string.IsNullOrWhiteSpace(paymentDetailDTO.LastFourDigits))
                paymentDetail.LastFourDigits = paymentDetailDTO.LastFourDigits;

            if (!string.IsNullOrWhiteSpace(paymentDetailDTO.CardType))
                paymentDetail.CardType = paymentDetailDTO.CardType;

            if (!string.IsNullOrWhiteSpace(paymentDetailDTO.ExpirationDate))
                paymentDetail.ExpirationDate = paymentDetailDTO.ExpirationDate;

            if (paymentDetailDTO.PaymentId > 0)
                paymentDetail.PaymentID = paymentDetailDTO.PaymentId;
        }
    }
}
