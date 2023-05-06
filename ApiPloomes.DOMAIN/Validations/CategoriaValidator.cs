using ApiPloomes.DOMAIN.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Validations
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado");
        }
    }
}
