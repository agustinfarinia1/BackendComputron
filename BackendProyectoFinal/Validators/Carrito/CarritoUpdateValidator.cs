using BackendProyectoFinal.DTOs.CarritoDTO;
using FluentValidation;

namespace BackendProyectoFinal.Validators.Carrito
{
    public class CarritoUpdateValidator : AbstractValidator<CarritoUpdateDTO>
    {
        public CarritoUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El ID es obligatorio");
            RuleFor(x => x.UsuarioId).NotEmpty().WithMessage("El UsuarioId es obligatorio");
            RuleFor(x => x.ListaCarrito).Empty().WithMessage("La lista carrito debe tener contenido");
        }
    }
}
