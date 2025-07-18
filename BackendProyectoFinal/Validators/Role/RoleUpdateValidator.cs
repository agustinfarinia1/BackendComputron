using BackendProyectoFinal.DTOs.Role;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Role
{
    public class RoleUpdateValidator : AbstractValidator<RoleUpdateDTO>
    {
        public RoleUpdateValidator() 
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
