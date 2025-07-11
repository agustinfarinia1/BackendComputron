using BackendProyectoFinal.DTOs.BrandDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Brand
{
    public class BrandUpdateValidator : AbstractValidator<BrandUpdateDTO>
    {
        public BrandUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
