using BackendProyectoFinal.DTOs;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Usuario
{
    public class UsuarioUpdateValidator : AbstractValidator<UsuarioUpdateDTO>
    {
        public UsuarioUpdateValidator() 
        {
            var fechaMinima = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
            RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Nombre).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La Password es obligatorias");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es obligatorio");
            RuleFor(x => x.Apellido).Length(2, 20).WithMessage("El apellido debe medir de 2 a 20 caracteres");
            RuleFor(x => x.Email).NotEmpty().WithMessage("El email es obligatorio");
            RuleFor(x => x.Email).Length(10, 50).WithMessage("El apellido debe medir de 10 a 50 caracteres");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Debe tener el cuerpo correspondiente de email");
            RuleFor(x => x.FechaNacimiento).NotEmpty().WithMessage("La fecha de nacimiento es obligatoria");
            RuleFor(x => x.FechaNacimiento).LessThanOrEqualTo(fechaMinima).WithMessage("El usuario debe ser mayor de 18 años");
            RuleFor(x => x.RolID).NotEmpty().WithMessage("El usuario debe tener rol");
            RuleFor(x => x.DomicilioID).NotEmpty().WithMessage("El usuario debe tener domicilio");
            RuleFor(x => x.Eliminado).NotNull().WithMessage("El usuario debe tener estado");
        }
    }
}
