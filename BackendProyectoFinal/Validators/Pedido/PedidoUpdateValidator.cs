using BackendProyectoFinal.DTOs.PedidoDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Pedido
{
    public class PedidoUpdateValidator : AbstractValidator<PedidoUpdateDTO>
    {
        public PedidoUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("El Usuario ligado es obligatorio");
            RuleFor(x => x.EstadoPedidoID).NotEmpty().WithMessage("El Estado Pedido es obligatorio");
            RuleFor(x => x.DomicilioId).NotEmpty().WithMessage("El Domicilio de entrega es obligatorio");
        }

    }
}
