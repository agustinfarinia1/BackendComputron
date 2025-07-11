using FluentValidation;
using BackendProyectoFinal.DTOs.CategoryDTO;

namespace BackendProyectoFinal.Validators.Category
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateValidator() 
        {
            RuleFor(x => x.Id).NotNull().WithMessage(x => "El Id es obligatorio");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
