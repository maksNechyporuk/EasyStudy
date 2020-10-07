using System;
using System.Linq;
using EasyStudy.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace EasyStudy.DAL.Seeder
{
    public class StudentSeeder
    {
        public static void SeedStudent(UserManager<DbUser> userManager,
            RoleManager<DbRole> roleManager)
        {
            var count = roleManager.Roles.Count();
            var roleName = "Student";
            if (roleManager.FindByNameAsync(roleName).Result == null)
            {
                var result = roleManager.CreateAsync(new DbRole
                {
                    Name = roleName
                }).Result;
            }
            string email = "beatrice.lavigne@example.com";
            Random random=new Random();
            
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                Student student =new Student()
                    {  
                        Age = random.Next(6,18),
                        Image = "https://randomuser.me/api/portraits/women/10.jpg"
                        ,FirstName = "Deniz",
                        LastName = "Adan",
                        MiddleName = "Іванович" , 
                        GroupId= 2
                    };
                var user = new DbUser()
                {
                    Email = email,
                    UserName =email,
                    PhoneNumber = "(700)-647-0341",
                    Student = student
                    
                };
                var result = userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
            email = "lucas.gagne@example.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                Student student =new Student()
                {  
                    Age = random.Next(6,18),
                    Image = "https://randomuser.me/api/portraits/women/10.jpg"   
                    ,FirstName = "Branco",
                    LastName = "Messemaker",
                    MiddleName = "Іванович"
                    , GroupId = 2
                };
                var user = new DbUser()
                {
                    Email = email,
                    UserName =email,
                    PhoneNumber = "(700)-482-348",
                    Student = student
                    
                };
                var result = userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
            email = "leevi.niemi@example.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                Student student =new Student()
                {  
                    Age = random.Next(6,18),
                    Image = "https://randomuser.me/api/portraits/women/10.jpg"
                    ,FirstName = "Marius",
                    LastName = "Johansen",
                    MiddleName = "Іванович"
                    , GroupId = 1
                };
                var user = new DbUser()
                {
                    Email = email,
                    UserName =email,
                    PhoneNumber = "(700)-647-7842",
                    Student = student
                    
                };
                var result = userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
            email = "luukas.hautala@example.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                Student student =new Student()
                {  
                    Age = random.Next(6,18),
                    Image = "https://randomuser.me/api/portraits/women/10.jpg"
                    ,FirstName = "Fletcher",
                    LastName = "Wright",
                    MiddleName = "Іванович"   
                    , GroupId = 1
                        
                };
                var user = new DbUser()
                {
                    Email = email,
                    UserName =email,
                    PhoneNumber = "(700)-123-6544",
                    Student = student
                    
                };
                var result = userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
            email = "gul.kocoglu@example.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                Student student =new Student()
                {  
                    Age = random.Next(6,18),
                    Image = "https://randomuser.me/api/portraits/women/10.jpg"
                    ,FirstName = "Beatrice",
                    LastName = "Lavigne",
                    MiddleName = "Іванович",
                    GroupId= 1
                };
                var user = new DbUser()
                {
                    Email = email,
                    UserName =email,
                    PhoneNumber = "(700)-671-8743",
                    Student = student
                };
                var result = userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = userManager.AddToRoleAsync(user, roleName).Result;
            }
        }
    }
}
