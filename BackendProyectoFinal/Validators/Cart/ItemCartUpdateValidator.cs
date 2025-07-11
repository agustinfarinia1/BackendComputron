using FluentValidation;
using BackendProyectoFinal.DTOs.ItemCartDTO;

namespace BackendProyectoFinal.Validators.Cart
{
    public class ItemCartUpdateValidator : AbstractValidator<ItemCartUpdateDTO>
    {
        public ItemCartUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El Item Producto ID es obligatorio.");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("El Producto ID es obligatorio.");
            RuleFor(x => x.CartId).NotEmpty().WithMessage("El Carrito ID es obligatorio.");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("El Domicilio de entrega es obligatorio.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor.");
        }
    }
}
