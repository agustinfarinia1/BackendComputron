using BackendProyectoFinal.DTOs.RoleDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Role
{
    public class RoleInsertValidator : AbstractValidator<RoleInsertDTO>
    {
        public RoleInsertValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2,20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
