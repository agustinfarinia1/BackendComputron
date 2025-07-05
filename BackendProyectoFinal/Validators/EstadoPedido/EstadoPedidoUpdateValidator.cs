using BackendProyectoFinal.DTOs.EstadoPedidoDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.EstadoPedido
{
    public class EstadoPedidoUpdateValidator : AbstractValidator<EstadoPedidoUpdateDTO>
    {
        public EstadoPedidoUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Nombre).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
