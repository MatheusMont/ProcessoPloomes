using ApiPloomes.DOMAIN.Models;
using ApiPloomes.DOMAIN.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiPloomes.TESTS.UnitTests
{
    public class CategoriaTests
    {
        [Theory(DisplayName = "Validar Categoria Válida")]
        [Trait("Type", "Categoria")]
        [InlineData("Eletrônicos")]
        [InlineData("Veículos")]
        public void Validate_ValidCategoria_MustBeValid(string nome)
        {
            // Arrange
            var _categoriaValidator = new CategoriaValidator();

            var categoria = new Categoria
            {
                Nome = nome
            };

            // Act
            var result = _categoriaValidator.Validate(categoria);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory(DisplayName = "Validar Categoria Inválida")]
        [Trait("Type", "Categoria")]
        [InlineData("")]
        [InlineData(null)]
        public void Validate_InvalidCategoria_MustBeInvalid(string nome)
        {
            // Arrange
            var _categoriaValidator = new CategoriaValidator();

            var categoria = new Categoria
            {
                Nome = nome
            };

            // Act
            var result = _categoriaValidator.Validate(categoria);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
