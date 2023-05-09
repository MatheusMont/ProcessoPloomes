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
    public class UserMockData
    {
        public static async Task CreateUsers(DatabaseApiApplication application, bool create)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                using (var testsDbContext = provider.GetRequiredService<PloomesContext>())
                {
                    await testsDbContext.Database.EnsureCreatedAsync();


                    var user1 = new User();

                    user1.Username = "Username1";
                    user1.Password = BCrypt.Net.BCrypt.HashPassword("User1ValidPassword");
                    user1.Email = "user1@mail.com";

                    var user2 = new User();
                    user2.Username = "Username2";
                    user2.Password = BCrypt.Net.BCrypt.HashPassword("User2ValidPassword");
                    user2.Email = "user2@mail.com";


                    if (create)
                    {
                        await testsDbContext.Users.AddAsync(user1);
                        await testsDbContext.Users.AddAsync(user2);
                        await testsDbContext.SaveChangesAsync();
                    }
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
    }
}

