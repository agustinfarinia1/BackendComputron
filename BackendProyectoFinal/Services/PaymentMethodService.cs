using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.Payment.PaymentMethod;

namespace BackendProyectoFinal.Services
{
    public class PaymentMethodService : ICommonService<PaymentMethodDTO, PaymentMethodInsertDTO, PaymentMethodUpdateDTO>
    {
        private IRepository<PaymentMethod> _repository;
        public List<string> Errors { get; }
        public PaymentMethodService(IRepository<PaymentMethod> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<PaymentMethodDTO>> Get()
        {
            var paymentMethods = await _repository.Get();
            // Convierte los PaymentMethods A DTOs
            return paymentMethods.Select(paymentMethod =>
            PaymentMethodMapper.ConvertPaymentMethodToDTO(paymentMethod)
            );
        }

        public async Task<PaymentMethodDTO?> GetById(int id)
        {
            var paymentMethod = await _repository.GetById(id);
            if (paymentMethod != null)
            {
                return PaymentMethodMapper.ConvertPaymentMethodToDTO(paymentMethod);
            }
            return null;
        }

        public async Task<PaymentMethodDTO?> GetByField(string field)
        {
            // Busca filtrando por Name
            var paymentMethod = _repository.Search(p => p.Name.ToUpper() == field.ToUpper()).FirstOrDefault();
            if (paymentMethod != null)
            {
                return PaymentMethodMapper.ConvertPaymentMethodToDTO(paymentMethod);
            }
            return null;
        }

        public async Task<PaymentMethodDTO> Add(PaymentMethodInsertDTO paymentMethodInsertDTO)
        {
            var paymentMethod = new PaymentMethod()
            {
                Name = paymentMethodInsertDTO.Name
            };
            await _repository.Add(paymentMethod);
            await _repository.Save();

            return PaymentMethodMapper.ConvertPaymentMethodToDTO(paymentMethod);
        }

        public async Task<PaymentMethodDTO?> Update(PaymentMethodUpdateDTO paymentMethodUpdateDTO)
        {
            var paymentMethod = await _repository.GetById(paymentMethodUpdateDTO.Id);
            if (paymentMethod != null)
            {
                paymentMethod.Name = paymentMethodUpdateDTO.Name;

                _repository.Update(paymentMethod);
                await _repository.Save();

                return PaymentMethodMapper.ConvertPaymentMethodToDTO(paymentMethod);
            }
            return null;
        }

        public async Task<PaymentMethodDTO?> Delete(int id)
        {
            var paymentMethod = await _repository.GetById(id);
            if (paymentMethod != null)
            {
                var paymentMethodDTO = PaymentMethodMapper.ConvertPaymentMethodToDTO(paymentMethod);

                _repository.Delete(paymentMethod);
                await _repository.Save();
                return paymentMethodDTO;
            }
            return null;
        }

        public bool Validate(PaymentMethodInsertDTO paymentMethodDTO)
        {
            if (_repository.Search(p => p.Name.ToUpper() == paymentMethodDTO.Name.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir un Payment Method con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(PaymentMethodUpdateDTO paymentMethodUpdateDTO)
        {
            if (_repository.Search(
                p => p.Name.ToUpper() == paymentMethodUpdateDTO.Name.ToUpper()
                && paymentMethodUpdateDTO.Id != p.PaymentMethodID).Count() > 0)
            {
                Errors.Add("No puede existir un Payment Method con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
