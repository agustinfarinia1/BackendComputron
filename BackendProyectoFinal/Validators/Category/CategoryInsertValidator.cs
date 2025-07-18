using FluentValidation;
using BackendProyectoFinal.DTOs.Category;

namespace BackendProyectoFinal.Validators.Category
{
    public class CategoryInsertValidator : AbstractValidator<CategoryInsertDTO>
    {
        public CategoryInsertValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Name).Length(2,20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
