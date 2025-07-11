﻿using FluentValidation;
using BackendProyectoFinal.DTOs.UserDTO;

namespace BackendProyectoFinal.Validators.User
{
    public class UserInsertValidator : AbstractValidator<UserInsertDTO>
    {
        public UserInsertValidator() 
        {
            var fechaMinima = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(x => x.FirstName).Length(2,20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La Password es obligatorias");
            RuleFor(x => x.SurName).NotEmpty().WithMessage("El apellido es obligatorio");
            RuleFor(x => x.SurName).Length(2, 20).WithMessage("El apellido debe medir de 2 a 20 caracteres");
            RuleFor(x => x.Email).NotEmpty().WithMessage("El email es obligatorio");
            RuleFor(x => x.Email).Length(10, 50).WithMessage("El apellido debe medir de 10 a 50 caracteres");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Debe tener el cuerpo correspondiente de email");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("La fecha de nacimiento es obligatoria");
            RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(fechaMinima).WithMessage("El usuario debe ser mayor de 18 años");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("El usuario debe tener rol");
            RuleFor(x => x.AddressId).NotEmpty().WithMessage("El usuario debe tener domicilio");
        }
    }
}
