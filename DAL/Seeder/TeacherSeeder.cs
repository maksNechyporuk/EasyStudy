using System;
using System.Linq;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Seeder
{
    public class TeacherSeeder
    {
        public static void SeedStudent(UserManager<DbUser> userManager,
           RoleManager<DbRole> roleManager)
        {
            var count = roleManager.Roles.Count();
            var roleName = "Teacher";
            if (roleManager.FindByNameAsync(roleName).Result == null)
            {
                var result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
            }
            string email = "jacira.goncalves@example.com";
            Random random = new Random();
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                Teacher teacher = new Teacher()
                {
                    DayOfbirthday = DateTime.Now,
                    Image = "https://randomuser.me/api/portraits/women/96.jpg",
                    FirstName = "Jacira",
                    LastName = "Gonçalves",
                    MiddleName = "Іванович",
                    GroupId = 1

                };
                var user = new DbUser()
                {
                    Email = email,
                    UserName = email,
                    PhoneNumber = "(50) 9602-6972",
                    Teacher = teacher
                };
                var result = userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
            email = "levi.evans@example.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                Teacher teacher = new Teacher()
                {
                    DayOfbirthday = DateTime.Now,
                    Image = "https://randomuser.me/api/portraits/men/83.jpg",
                    FirstName = "Levi",
                    LastName = "Evans",
                    MiddleName = "Іванович",
                    GroupId = 2
                };
                var user = new DbUser()
                {
                    Email = email,
                    UserName = email,
                    PhoneNumber = "(519)-347-8485",
                    Teacher = teacher
                };
                var result = userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
        }
    }
}
