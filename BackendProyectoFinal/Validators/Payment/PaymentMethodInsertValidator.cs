using FluentValidation;
using BackendProyectoFinal.DTOs.Payment.PaymentMethod;

namespace BackendProyectoFinal.Validators.Payment
{
    public class PaymentMethodInsertValidator : AbstractValidator<PaymentMethodInsertDTO>
    {
        public PaymentMethodInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El Name es obligatorio");
        }
    }
}
