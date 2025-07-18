using FluentValidation;
using BackendProyectoFinal.DTOs.Product;

namespace BackendProyectoFinal.Validators.Product
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio");
            RuleFor(x => x.Title).NotEmpty().WithMessage("El titulo es obligatorio");
            RuleFor(x => x.Title).Length(2, 50).WithMessage("El titulo debe medir de 2 a 20 caracteres");
            RuleFor(x => x.BrandId).NotEmpty().WithMessage("La marca es obligatoria");
            RuleFor(x => x.Price).NotEmpty().WithMessage("La Password es obligatorias");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(1000).WithMessage("El precio debe ser mayor a 1000 pesos");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("La cantidad es obligatoria");
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1).WithMessage("La cantidad debe ser mayor a cero");
            RuleFor(x => x.Image).NotEmpty().WithMessage("La imagen es obligatoria");
            RuleFor(x => x.CreationDate).NotEmpty().WithMessage("La fecha de creacion es obligatoria");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("La categiria de producto es obligatoria");
            RuleFor(x => x.MLCode).NotEmpty().WithMessage("El codigo de Mercado Libre es obligatorio");
            RuleFor(x => x.Eliminated).NotNull().WithMessage("El Producto debe tener estado");
        }
    }
}
