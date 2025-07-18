using FluentValidation;
using BackendProyectoFinal.DTOs.ItemOrder;

namespace BackendProyectoFinal.Validators.Order
{
    public class ItemOrderUpdateValidator : AbstractValidator<ItemOrderUpdateDTO>
    {
        public ItemOrderUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El Item Producto ID es obligatorio.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("El Producto ID es obligatorio.");
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("El Pedido ID es obligatorio.");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("La Cantidad es obligatoria.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor.");
        }
    }
}
