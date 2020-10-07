using System;
using EasyStudy.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyStudy.DAL.Seeder
{
    public class SeederDB
    {
        public static void SeedData(IServiceProvider services,
            IWebHostEnvironment env,
            IConfiguration config)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                var context = scope.ServiceProvider.GetRequiredService<EFContext>();
                TeacherSeeder.SeedStudent(manager, managerRole);
                GroupSeeder.SeedCategories(context);

                StudentSeeder.SeedStudent(manager, managerRole);


            }
        }
    }
}
