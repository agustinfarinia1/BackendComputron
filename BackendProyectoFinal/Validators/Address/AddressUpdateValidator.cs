using FluentValidation;
using BackendProyectoFinal.DTOs.AddressDTO;

namespace BackendProyectoFinal.Validators.Address
{
    public class AddressUpdateValidator : AbstractValidator<AddressUpdateDTO>
    {
        public AddressUpdateValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
            RuleFor(x => x.Number).NotEmpty().WithMessage("El numero es obligatorio");
            RuleFor(x => x.Number).GreaterThan(0).WithMessage("El numero debe ser mayor que 0");
        }
    }
}
