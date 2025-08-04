using FluentValidation;
using BackendProyectoFinal.DTOs.Cart.ItemCart;


namespace BackendProyectoFinal.Validators.Cart
{
    public class ItemCartInsertValidator : AbstractValidator<ItemCartInsertDTO>
    {
        public ItemCartInsertValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("El Producto ID es obligatorio.");
            RuleFor(x => x.CartId).NotEmpty().WithMessage("El Carrito ID es obligatorio.");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("La Cantidad es obligatoria.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor.");
        }
    }
}
