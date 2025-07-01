using BackendProyectoFinal.DTOs;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Rol
{
    public class RolUpdateValidator : AbstractValidator<RolUpdateDTO>
    {
        public RolUpdateValidator() 
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Nombre).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
