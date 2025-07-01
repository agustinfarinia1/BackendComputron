using BackendProyectoFinal.DTOs;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Domicilio
{
    public class DomicilioInsertValidator : AbstractValidator<DomicilioInsertDTO>
    {
        public DomicilioInsertValidator() 
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Nombre).Length(2,20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
            RuleFor(x => x.Numero).NotEmpty().WithMessage("El numero es obligatorio");
            RuleFor(x => x.Numero).GreaterThan(0).WithMessage("El numero debe ser mayor que 0");
        }
    }
}
