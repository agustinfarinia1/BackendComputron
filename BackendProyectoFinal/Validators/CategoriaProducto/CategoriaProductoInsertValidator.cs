﻿using BackendProyectoFinal.DTOs;
using FluentValidation;

namespace BackendProyectoFinal.Validators.CategoriaProducto
{
    public class CategoriaProductoInsertValidator : AbstractValidator<CategoriaProductoInsertDTO>
    {
        public CategoriaProductoInsertValidator() 
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.Nombre).Length(2,20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
        }
    }
}
