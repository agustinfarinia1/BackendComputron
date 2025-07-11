using FluentValidation;
using BackendProyectoFinal.DTOs.OrderStatusDTO;

namespace BackendProyectoFinal.Validators.Order
{
    public class OrderStatusInsertValidator : AbstractValidator<OrderStatusInsertDTO>
    {
        public OrderStatusInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El Nombre es obligatorio");
        }
    }
}
