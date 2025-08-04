using FluentValidation;
using BackendProyectoFinal.DTOs.Payment.PaymentMethod;

namespace BackendProyectoFinal.Validators.Payment
{
    public class PaymentMethodUpdateValidator : AbstractValidator<PaymentMethodUpdateDTO>
    {
        public PaymentMethodUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El Name es obligatorio");
        }
    }
}
