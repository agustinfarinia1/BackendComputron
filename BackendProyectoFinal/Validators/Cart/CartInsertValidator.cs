using BackendProyectoFinal.DTOs.Cart;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Cart
{
    public class CartInsertValidator : AbstractValidator<CartInsertDTO>
    {
        public CartInsertValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("El Usuario ID es obligatorio");
        }
    }
}
