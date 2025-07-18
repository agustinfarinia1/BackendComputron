using FluentValidation;
using BackendProyectoFinal.DTOs.Cart;

namespace BackendProyectoFinal.Validators.Cart
{
    public class CartUpdateValidator : AbstractValidator<CartUpdateDTO>
    {
        public CartUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("El Usuario ID es obligatorio");
        }
    }
}
