using ApiPloomes.DOMAIN.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Validations
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(p => p.CategoriaId)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado");

            RuleFor(p => p.UsuarioId)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado");

            RuleFor(p => p.Descricao)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado");

            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado");

            RuleFor(p => p.Preco)
                .NotEmpty()
                .WithMessage("{PropertyName} deve ser informado")
                .GreaterThan(0)
                .WithMessage("{PropertyName} deve ser um valor positivo e maior que 0");
        }
    }
}
