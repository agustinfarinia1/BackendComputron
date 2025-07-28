using FluentValidation;
using BackendProyectoFinal.DTOs.Order;

namespace BackendProyectoFinal.Validators.Order
{
    public class OrderInsertValidator : AbstractValidator<OrderInsertDTO>
    {
        public OrderInsertValidator()
        {
            RuleFor(x => x.AddressId).NotEmpty().WithMessage("El Domicilio es obligatorio");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("El Usuario ID es obligatorio");
        }

    }
}
