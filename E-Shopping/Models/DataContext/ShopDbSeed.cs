using E_Shopping.Models.Entity.Membership;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping.Models.DataContext
{
    static public class ShopDbSeed
    {
        static internal IApplicationBuilder Seed(this IApplicationBuilder builder)
        {
            return builder;
        }

        static internal IApplicationBuilder SeedIdentity(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ShopUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ShopRole>>();

                ShopRole superAdminRole = roleManager.FindByNameAsync("Admin").Result;

                if (superAdminRole == null)
                {
                    superAdminRole = new ShopRole
                    {
                        Name = "Admin"
                    };
                    roleManager.CreateAsync(superAdminRole).Wait();
                }

                ShopUser superAdminUser = userManager.FindByEmailAsync("abbasam@code.edu.az").Result;

                if (superAdminUser == null)
                {
                    superAdminUser = new ShopUser
                    {
                        Email = "abbasam@code.edu.az",
                        UserName = "abbasmuradzada",
                        EmailConfirmed = true
                    };

                    var resultUser = userManager.CreateAsync(superAdminUser, "123").Result;

                    if (resultUser.Succeeded)
                    {
                        var roleResult = userManager.AddToRoleAsync(superAdminUser, "Admin").Result;
                    }


                }
                else if (!userManager.IsInRoleAsync(superAdminUser, "Admin").Result)
                {
                    userManager.AddToRoleAsync(superAdminUser, "Admin").Wait();
                }

            }


            return builder;
        }
    }
}
