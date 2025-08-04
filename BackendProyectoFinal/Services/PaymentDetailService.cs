using BackendProyectoFinal.DTOs.Payment.PaymentDetail;
using BackendProyectoFinal.DTOs.User;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class PaymentDetailService : ICommonService<PaymentDetailDTO, PaymentDetailInsertDTO, PaymentDetailUpdateDTO>
    {
        private IRepository<PaymentDetail> _repository;
        public List<string> Errors { get; }

        private readonly EncryptService _encryptService;
        public PaymentDetailService(
            IRepository<PaymentDetail> repository,
            [FromKeyedServices("EncryptService")] EncryptService encryptService)
        {
            _repository = repository;
            _encryptService = encryptService;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<PaymentDetailDTO>> Get()
        {
            var paymentDetails = await _repository.Get();
            // Convierte los PaymentDetails A DTO
            return paymentDetails.Select(paymentDetail =>
            PaymentDetailMapper.ConvertPaymentDetailToDTO(paymentDetail)
            );
        }

        public async Task<PaymentDetailDTO?> GetById(int id)
        {
            var paymentDetail = await _repository.GetById(id);
            if (paymentDetail != null)
            {
                return PaymentDetailMapper.ConvertPaymentDetailToDTO(paymentDetail);
            }
            return null;
        }

        public async Task<PaymentDetailDTO?> GetByField(string field)
        {
            // Filtra por Name
            var paymentDetail = _repository.Search(p => p.PaymentID == int.Parse(field)).FirstOrDefault();
            if (paymentDetail != null)
            {
                return PaymentDetailMapper.ConvertPaymentDetailToDTO(paymentDetail);
            }
            return null;
        }

        public async Task<PaymentDetailDTO> Add(PaymentDetailInsertDTO paymentDetailInsertDTO)
        {
            var encryptCardHolderName = _encryptService.EncryptData(paymentDetailInsertDTO.CardHolderName);
            paymentDetailInsertDTO.CardHolderName = encryptCardHolderName;
            var encryptLastFourDigits = _encryptService.EncryptData(paymentDetailInsertDTO.LastFourDigits);
            paymentDetailInsertDTO.LastFourDigits = encryptLastFourDigits;
            var encryptExpirationDate = _encryptService.EncryptData(paymentDetailInsertDTO.ExpirationDate.ToString());
            paymentDetailInsertDTO.ExpirationDate = encryptExpirationDate;
            var encryptCardType = _encryptService.EncryptData(paymentDetailInsertDTO.CardType);
            paymentDetailInsertDTO.CardType = encryptCardType;

            var paymentDetail = PaymentDetailMapper.ConvertDTOToModel(paymentDetailInsertDTO);
            await _repository.Add(paymentDetail);
            await _repository.Save();

            return PaymentDetailMapper.ConvertPaymentDetailToDTO(paymentDetail);
        }

        public async Task<PaymentDetailDTO?> Update(PaymentDetailUpdateDTO paymentDetailUpdateDTO)
        {
            var paymentDetail = await _repository.GetById(paymentDetailUpdateDTO.Id);
            if (paymentDetail != null)
            {
                PaymentDetailMapper.UpdatePaymentDetail(paymentDetail,paymentDetailUpdateDTO);

                _repository.Update(paymentDetail);
                await _repository.Save();

                return PaymentDetailMapper.ConvertPaymentDetailToDTO(paymentDetail);
            }
            return null;
        }

        public async Task<PaymentDetailDTO?> Delete(int id)
        {
            var paymentDetail = await _repository.GetById(id);
            if (paymentDetail != null)
            {
                var paymentDetailDTO = PaymentDetailMapper.ConvertPaymentDetailToDTO(paymentDetail);

                _repository.Delete(paymentDetail);
                await _repository.Save();
                return paymentDetailDTO;
            }
            return null;
        }

        public bool Validate(PaymentDetailInsertDTO paymentDetailDTO)
        {
            if (_repository.Search(p
                => p.PaymentID == paymentDetailDTO.PaymentId)
                .Count() > 0)
            {
                Errors.Add("No puede existir dos PaymentDetail con PaymentId repetidos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(PaymentDetailUpdateDTO paymentDetailDTO)
        {
            if (_repository.Search(p
                => p.PaymentID == paymentDetailDTO.PaymentId
                && paymentDetailDTO.Id != p.PaymentDetailID)
                .Count() > 0)
            {
                Errors.Add("No puede existir dos PaymentDetail con PaymentId repetidos");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
