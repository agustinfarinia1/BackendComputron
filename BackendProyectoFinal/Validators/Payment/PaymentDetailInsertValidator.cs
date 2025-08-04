using BackendProyectoFinal.DTOs.Payment.PaymentDetail;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Payment
{
    public class PaymentDetailInsertValidator : AbstractValidator<PaymentDetailInsertDTO>
    {
        public PaymentDetailInsertValidator()
        {
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("El CardHolderName es obligatorio");
            RuleFor(x => x.LastFourDigits).NotEmpty().WithMessage("El LastFourDigits es obligatorio");
            RuleFor(x => x.CardType).NotEmpty().WithMessage("El CardType es obligatorio");
            RuleFor(x => x.PaymentId).NotEmpty().WithMessage("El PaymentID es obligatorio");
        }
    }
}
