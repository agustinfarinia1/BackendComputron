using BackendProyectoFinal.DTOs.Cart;
using BackendProyectoFinal.DTOs.Payment;
using BackendProyectoFinal.DTOs.User.Role;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class PaymentService : ICommonService<PaymentDTO, PaymentInsertDTO, PaymentUpdateDTO>
    {
        private IRepository<Payment> _repository;
        public List<string> Errors { get; }
        public PaymentService(IRepository<Payment> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<PaymentDTO>> Get()
        {
            var payments = await _repository.Get();
            // Convierte los Payments A DTO
            return payments.Select(payment =>
            PaymentMapper.ConvertPaymentToDTO(payment)
            );
        }

        public async Task<PaymentDTO?> GetById(int id)
        {
            var payment = await _repository.GetById(id);
            if (payment != null)
            {
                return PaymentMapper.ConvertPaymentToDTO(payment);
            }
            return null;
        }

        public async Task<PaymentDTO?> GetByField(string field)
        {
            var payment = _repository.Search(p => p.OrderID == int.Parse(field)).FirstOrDefault();
            if (payment != null)
            {
                return PaymentMapper.ConvertPaymentToDTO(payment);
            }
            return null;
        }

        public async Task<PaymentDTO> Add(PaymentInsertDTO PaymentInsertDTO)
        {
            var payment = PaymentMapper.ConvertDTOToModel(PaymentInsertDTO);
            await _repository.Add(payment);
            await _repository.Save();

            return PaymentMapper.ConvertPaymentToDTO(payment);
        }

        public async Task<PaymentDTO?> Update(PaymentUpdateDTO paymentUpdateDTO)
        {
            var payment = await _repository.GetById(paymentUpdateDTO.Id);
            if (payment != null)
            {
                PaymentMapper.UpdatePayment(payment,paymentUpdateDTO);

                _repository.Update(payment);
                await _repository.Save();

                return PaymentMapper.ConvertPaymentToDTO(payment);
            }
            return null;
        }

        public async Task<PaymentDTO?> Delete(int id)
        {
            var payment = await _repository.GetById(id);
            if (payment != null)
            {
                var paymentDTO = PaymentMapper.ConvertPaymentToDTO(payment);

                _repository.Delete(payment);
                await _repository.Save();
                return paymentDTO;
            }
            return null;
        }

        public bool Validate(PaymentInsertDTO paymentDTO)
        {
            if (_repository.Search(p
             => p.OrderID == paymentDTO.OrderId)
             .Count() > 0)
            {
                Errors.Add("No puede existir un Payment con un OrderId ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(PaymentUpdateDTO paymentDTO)
        {
            if (_repository.Search(p
                => p.OrderID == paymentDTO.OrderId
                && paymentDTO.Id != p.PaymentID)
                .Count() > 0)
            {
                Errors.Add("No puede existir un Payment con un OrderId ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
