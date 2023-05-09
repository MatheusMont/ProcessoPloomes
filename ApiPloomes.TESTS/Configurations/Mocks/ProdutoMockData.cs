using ApiPloomes.DATA.Context;
using ApiPloomes.DOMAIN.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.TESTS.Configurations.Mocks
{
    public class ProdutoMockData
    {
        public static async Task CreateProdutos(DatabaseApiApplication application, bool create)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                using (var integrationDbContext = provider.GetRequiredService<PloomesContext>())
                {
                    await integrationDbContext.Database.EnsureCreatedAsync();

                    await UserMockData.CreateUsers(application, true);
                    await CategoriaMockData.CreateCategorias(application, true);

                    var usersId = await UserMockData.GetUsersId(application);
                    var categoriasId = await CategoriaMockData.GetCategoriasId(application);

                    var produto1 = new Produto();
                    produto1.Nome = "Celular";
                    produto1.Descricao = "Aparelho Android com 64gb";
                    produto1.Preco = 1200;
                    produto1.UsuarioId = usersId[0];
                    produto1.CategoriaId = categoriasId[0];

                    var produto2 = new Produto();
                    produto2.Nome = "Notebook";
                    produto2.Descricao = "Notebook 1tb";
                    produto2.Preco = 1500;
                    produto2.UsuarioId = usersId[1];
                    produto2.CategoriaId = categoriasId[0];

                    var produto3 = new Produto();
                    produto3.Nome = "Sofá";
                    produto3.Descricao = "Sofá 2m";
                    produto3.Preco = 2000;
                    produto3.UsuarioId = usersId[0];
                    produto3.CategoriaId = categoriasId[1];

                    var produto4 = new Produto();
                    produto4.Nome = "Armário";
                    produto4.Descricao = "Armário 4 portas";
                    produto4.Preco = 1000;
                    produto4.UsuarioId = usersId[1];
                    produto4.CategoriaId = categoriasId[1];


                    if (create)
                    {
                        await integrationDbContext.Produtos.AddAsync(produto1);
                        await integrationDbContext.Produtos.AddAsync(produto2);
                        await integrationDbContext.Produtos.AddAsync(produto3);
                        await integrationDbContext.Produtos.AddAsync(produto4);
                        await integrationDbContext.SaveChangesAsync();
                    }
                }
            }
        }

        public static async Task<List<Guid>> GetProdutosId(DatabaseApiApplication application)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                using (var userDbContext = provider.GetRequiredService<PloomesContext>())
                {
                    return userDbContext.Produtos.Select(u => u.Id).ToList();
                }
            }
        }

        public static async Task<List<Guid>> GetUsersId(DatabaseApiApplication application)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                using (var userDbContext = provider.GetRequiredService<PloomesContext>())
                {
                    return userDbContext.Users.Select(u => u.Id).ToList();
                }
            }
        }

        public static async Task<List<Guid>> GetCategoriasId(DatabaseApiApplication application)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                using (var userDbContext = provider.GetRequiredService<PloomesContext>())
                {
                    return userDbContext.Categorias.Select(u => u.Id).ToList();
                }
            }
        }
    }
}
