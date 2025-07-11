using FluentValidation;
using BackendProyectoFinal.DTOs.ItemOrderDTO;

namespace BackendProyectoFinal.Validators.Order
{
    public class ItemOrderUpdateValidator : AbstractValidator<ItemOrderUpdateDTO>
    {
        public ItemOrderUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El Item Producto ID es obligatorio.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("El Producto ID es obligatorio.");
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("El Pedido ID es obligatorio.");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("El Domicilio de entrega es obligatorio.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor.");
        }
    }
}
