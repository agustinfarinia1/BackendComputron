using BackendProyectoFinal.DTOs.Product.Brand;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Brand
{
    public class BrandInsertValidator : AbstractValidator<BrandInsertDTO>
    {
        public BrandInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
