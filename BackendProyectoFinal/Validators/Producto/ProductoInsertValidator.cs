using BackendProyectoFinal.DTOs.ProductoDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Producto
{
    public class ProductoInsertValidator : AbstractValidator<ProductoInsertDTO>
    {
        public ProductoInsertValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El titulo es obligatorio");
            RuleFor(x => x.Titulo).Length(2, 50).WithMessage("El titulo debe medir de 2 a 20 caracteres");
            RuleFor(x => x.MarcaId).NotEmpty().WithMessage("La marca es obligatoria");
            RuleFor(x => x.Precio).NotEmpty().WithMessage("La Password es obligatorias");
            RuleFor(x => x.Precio).GreaterThanOrEqualTo(1000).WithMessage("El precio debe ser mayor a 1000 pesos");
            RuleFor(x => x.Cantidad).NotEmpty().WithMessage("La cantidad es obligatoria");
            RuleFor(x => x.Cantidad).GreaterThanOrEqualTo(1).WithMessage("La cantidad debe ser mayor a cero");
            RuleFor(x => x.Imagen).NotEmpty().WithMessage("La imagen es obligatoria");
            RuleFor(x => x.FechaCreacion).NotEmpty().WithMessage("La fecha de creacion es obligatoria");
            RuleFor(x => x.CategoriaProductoID).NotEmpty().WithMessage("La categiria de producto es obligatoria");
            RuleFor(x => x.CodigoML).NotEmpty().WithMessage("El codigo de Mercado Libre es obligatorio");
            RuleFor(x => x.Eliminado).NotNull().WithMessage("El usuario debe tener estado");
        }
    }
}
