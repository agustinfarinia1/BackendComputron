using FluentValidation;
using BackendProyectoFinal.DTOs.Payment.PaymentDetail;

namespace BackendProyectoFinal.Validators.Payment
{
    public class PaymentDetailUpdateValidator : AbstractValidator<PaymentDetailUpdateDTO>
    {
        public PaymentDetailUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("El CardHolderName es obligatorio");
            RuleFor(x => x.LastFourDigits).NotEmpty().WithMessage("El LastFourDigits es obligatorio");
            RuleFor(x => x.CardType).NotEmpty().WithMessage("El CardType es obligatorio");
            RuleFor(x => x.PaymentId).NotEmpty().WithMessage("El PaymentID es obligatorio");
        }
    }
}
