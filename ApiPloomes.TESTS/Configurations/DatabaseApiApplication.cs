using ApiPloomes.DATA.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.TESTS.Configurations
{
    public class DatabaseApiApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<PloomesContext>));
                services.AddDbContext<PloomesContext>(options =>
                    options
                        .EnableServiceProviderCaching(false)
                        .UseInMemoryDatabase("ECommerceDatabase", root));
            });

            return base.CreateHost(builder);
        }
    }
}
