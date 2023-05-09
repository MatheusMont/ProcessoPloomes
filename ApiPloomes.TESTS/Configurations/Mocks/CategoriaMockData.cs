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
    public class CategoriaMockData
    {
        public static async Task CreateCategorias(DatabaseApiApplication application, bool create)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                using (var userDbContext = provider.GetRequiredService<PloomesContext>())
                {
                    await userDbContext.Database.EnsureCreatedAsync();


                    var categoria1 = new Categoria();

                    categoria1.Nome = "Eletrônicos";

                    var categoria2 = new Categoria();

                    categoria2.Nome = "Móveis";


                    if (create)
                    {
                        await userDbContext.Categorias.AddAsync(categoria1);
                        await userDbContext.Categorias.AddAsync(categoria2);
                        await userDbContext.SaveChangesAsync();
                    }
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
