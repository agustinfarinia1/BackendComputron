using BackendProyectoFinal.DTOs.MarcaDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Marca
{
    public class MarcaInsertValidator : AbstractValidator<MarcaInsertDTO>
    {
        public MarcaInsertValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Nombre).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
