using FluentValidation;
using BackendProyectoFinal.DTOs.OrderDTO;

namespace BackendProyectoFinal.Validators.Order
{
    public class OrderInsertValidator : AbstractValidator<OrderInsertDTO>
    {
        public OrderInsertValidator()
        {
            RuleFor(x => x.AddressId).NotEmpty().WithMessage("El Domicilio es obligatorio");
            RuleFor(x => x.OrderStatusId).NotEmpty().WithMessage("El Estado de Pedido es obligatorio");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("El Usuario ID es obligatorio");
        }

    }
}
