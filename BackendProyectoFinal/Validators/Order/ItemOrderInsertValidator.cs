using FluentValidation;
using BackendProyectoFinal.DTOs.Order.ItemOrder;

namespace BackendProyectoFinal.Validators.Order
{
    public class ItemOrderInsertValidator : AbstractValidator<ItemOrderInsertDTO>
    {
        public ItemOrderInsertValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("El Producto ID es obligatorio.");
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("El Pedido ID es obligatorio.");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("El Domicilio de entrega es obligatorio.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor.");
        }
    }
}
