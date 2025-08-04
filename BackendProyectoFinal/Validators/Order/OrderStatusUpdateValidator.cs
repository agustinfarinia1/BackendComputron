using BackendProyectoFinal.DTOs.Order.OrderStatus;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Order
{
    public class OrderStatusUpdateValidator : AbstractValidator<OrderStatusUpdateDTO>
    {
        public OrderStatusUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El Nombre es obligatorio");
        }
    }
}
