using ApiPloomes.DOMAIN.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado");

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado")
                .EmailAddress()
                .WithMessage("{PropertyName} deve ser um email válido");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado")
                .MinimumLength(8)
                .WithMessage("{PropertyName} deve ter no mínimo 8 caracteres")
                .MaximumLength(100)
                .WithMessage("{PropertyName} deve ter no máximo 100 caracteres")
                .Must(ValidatePassword)
                .WithMessage("{PropertyName} deve conter uma letra maiúscula, uma letra minúscula, um número e um símbolo [#, ?, !, @, $, %, ^, &, *, -]");

        }

        protected bool ValidatePassword(string password)
        {
            var validator = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\w\W]{0,}$");

            if (!validator.IsMatch(password)) return false;

            return true;
        }
    }
}
