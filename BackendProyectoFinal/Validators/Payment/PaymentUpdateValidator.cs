﻿using FluentValidation;
using BackendProyectoFinal.DTOs.Payment;

namespace BackendProyectoFinal.Validators.Payment
{
    public class PaymentUpdateValidator : AbstractValidator<PaymentUpdateDTO>
    {
        public PaymentUpdateValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("El Amount debe ser mayor a 0");
            RuleFor(x => x.PaymentMethodId).NotEmpty().WithMessage("El PaymentMethodId es obligatorio");
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("El OrderId es obligatorio");
        }
    }
}
