using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Entity;

namespace WebDemo.Common
{
    public class ClienteValidator : AbstractValidator<ClienteRequest>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.nombreCompleto)
                 .NotEmpty().WithMessage("{PropertyName} es requerido")
                .MaximumLength(200).WithMessage("{{PropertyName}} no debe exceder {MaxLength} caracteres")
                .Length(2, 200);

            RuleFor(x => x.nombreCorto)
                .NotEmpty().WithMessage("{PropertyName} es requerido")
                .MaximumLength(40).WithMessage("{{PropertyName}} no debe exceder {MaxLength} caracteres")
                .Length(2, 40);


            RuleFor(x => x.abreviatura)
                  .NotEmpty().WithMessage("{PropertyName} es requerido")
                .MaximumLength(40).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres");


            RuleFor(x => x.ruc).NotEmpty()
                .WithMessage("{PropertyName} es requerido")
                .Length(11).WithMessage("{PropertyName} debe tener {MaxLength} dígitos")
                .MaximumLength(11).WithMessage("{PropertyName} no debe exceder {MaxLength} dígitos")
                .Must(IsValidRuc).WithMessage("{PropertyName} únicamente dígitos.");

            RuleFor(x => x.estado)
                .NotEmpty().WithMessage("{PropertyName} es requerido")
                   .Length(1).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres");

            When(x => x.grupoFacturacion != null, () =>
            {
                RuleFor(x => x.grupoFacturacion)
                .NotEmpty().WithMessage("{PropertyName} es requerido")
            .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres");
            });


            When(x => x.codigoSap != null, () =>
            {
                RuleFor(x => x.codigoSap)
                .NotEmpty().WithMessage("{PropertyName} es requerido")
            .MaximumLength(100).WithMessage("{PropertyName} no debe exceder {MaxLength} caracteres");
            });

        }

        private bool IsValidName(string value)
        {

            return value.All(Char.IsLetter);
        }

        private bool IsValidRuc(string value)
        {
            return value.All(Char.IsDigit);
        }
    }
}
