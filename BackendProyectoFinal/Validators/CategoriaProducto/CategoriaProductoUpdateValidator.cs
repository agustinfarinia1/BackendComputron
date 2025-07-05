using BackendProyectoFinal.DTOs.CategoriaProductoDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.CategoriaProducto
{
    public class CategoriaProductoUpdateValidator : AbstractValidator<CategoriaProductoUpdateDTO>
    {
        public CategoriaProductoUpdateValidator() 
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Nombre).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
