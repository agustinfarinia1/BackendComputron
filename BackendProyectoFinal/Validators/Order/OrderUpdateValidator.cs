using FluentValidation;
using BackendProyectoFinal.DTOs.Order;

namespace BackendProyectoFinal.Validators.Order
{
    public class OrderUpdateValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
        }

    }
}
