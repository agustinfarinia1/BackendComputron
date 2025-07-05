using BackendProyectoFinal.DTOs.PedidoDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Pedido
{
    public class PedidoInsertValidator : AbstractValidator<PedidoInsertDTO>
    {
        public PedidoInsertValidator()
        {
            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("El Usuario ligado es obligatorio");
            RuleFor(x => x.EstadoPedidoID).NotEmpty().WithMessage("El Estado Pedido es obligatorio");
            RuleFor(x => x.DomicilioId).NotEmpty().WithMessage("El Domicilio de entrega es obligatorio");
        }

    }
}
