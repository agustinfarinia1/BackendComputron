using BackendProyectoFinal.DTOs.CarritoDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Carrito
{
    public class CarritoInsertValidator : AbstractValidator<CarritoInsertDTO>
    {
        public CarritoInsertValidator()
        {
            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("El UsuarioId es obligatorio");
            RuleFor(x => x.ListaCarrito).Empty().WithMessage("La lista carrito debe tener contenido");
        }
    }
}
