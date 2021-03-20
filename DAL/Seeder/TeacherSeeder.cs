using System;
using System.Linq;
using Bogus;
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
                    MiddleName = "Gonçalves",
                    GroupId = 1,
                    SchoolId = 1

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
                    MiddleName = "Evans",
                    GroupId = 2,
                    SchoolId = 1
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
            roleName = "Admin";
            if (roleManager.FindByNameAsync(roleName).Result == null)
            {
                var result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
            }
            email = "admin1@gmail.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                Teacher teacher = new Teacher()
                {
                    DayOfbirthday = DateTime.Now,
                    Image = "https://randomuser.me/api/portraits/men/83.jpg",
                    FirstName = "Levi",
                    LastName = "Evans",
                    MiddleName = "Evans",
                    SchoolId = 1
                };
                var user = new DbUser()
                {
                    Email = email,
                    UserName = email,
                    PhoneNumber = "(519)-347-8485",
                    Teacher = teacher
                };
                var result = userManager.CreateAsync(user, "Qwerty1234").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }

            //for (int i = 0; i < 100; i++)
            //{
            //    var fakerTeacker = new Faker<Teacher>()
            //        .RuleFor(c => c.FirstName, f => f.Person.FirstName)
            //        .RuleFor(c => c.LastName, f => f.Person.LastName)
            //        .RuleFor(c => c.DayOfbirthday, f => f.Person.DateOfBirth)
            //        .RuleFor(c => c.MiddleName, f => f.Person.FirstName)
            //        .RuleFor(c => c.Image, f => f.Person.Avatar);
            //    var fakerUser = new Faker<DbUser>()
            //        .RuleFor(c => c.Email, f => f.Person.Email)
            //        .RuleFor(c => c.PhoneNumber, f => f.Person.Phone);

            //    var user = fakerUser.Generate(1);
            //    user[0].Teacher = fakerTeacker.Generate(1)[0];
            //    user[0].UserName = user[0].Email;
            //    user[0].Teacher.SchoolId = 1;

            //    if (userManager.FindByEmailAsync(user[0].Email).Result == null)
            //    {
            //        var result = userManager.CreateAsync(user[0], "8Ki9x9-3of+s").Result;
            //        result = userManager.AddToRoleAsync(user[0], roleName).Result;
            //    }
            //}
        }
    }
}
