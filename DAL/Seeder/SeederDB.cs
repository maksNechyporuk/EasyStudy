using System;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Seeder
{
    public class SeederDB
    {
        public static void SeedData(IServiceProvider services,
            IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();
                TeacherSeeder.SeedStudent(userManager, managerRole);
                GroupSeeder.SeedCategories(context);

                StudentSeeder.SeedStudent(userManager, managerRole);
                var roleName = "Admin";
                if (managerRole.FindByNameAsync(roleName).Result == null)
                {
                    _ = managerRole.CreateAsync(new DbRole
                    {
                        Name = roleName
                    }).Result;
                }
                var user = new DbUser()
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    PhoneNumber = "(700)-647-0341",
                };
                if (userManager.FindByEmailAsync(user.Email).Result == null)
                {
                    _ = userManager.CreateAsync(user, "75Avudep").Result;
                    _ = userManager.AddToRoleAsync(user, roleName).Result;
                }
            }
        }
    }
}
