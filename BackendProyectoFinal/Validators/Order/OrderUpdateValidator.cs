using FluentValidation;
using BackendProyectoFinal.DTOs.Order;

namespace BackendProyectoFinal.Validators.Order
{
    public class OrderUpdateValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
            RuleFor(x => x.OrderStatusId).NotEmpty().WithMessage("El Estado de Pedido es obligatorio");
            RuleFor(x => x.AddressId).NotEmpty().WithMessage("El Domicilio es obligatorio");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("El Usuario ID es obligatorio");
        }

    }
}
