﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BLL.Models.TeacherModels;

namespace BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly EFContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly IGroupService _groupService;
        public StudentService(EFContext context,
            IConfiguration configuration,
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            IGroupService groupService)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _groupService = groupService;
        }
        public async Task<List<StudentVM>> GetStudentsByAge(int a, int b)
        {
            //var students = _context.Students.Where(st => st.DayOfbirthday >= a && st.DayOfbirthday <= b).AsQueryable();

            //return await GetJoinStudents(students);
            return null;
        }
        public async Task<List<StudentVM>> GetJoinStudents(IQueryable<Student> students)
        {
            var list = await students.Select(x =>
                new StudentVM
                {
                    Id = x.Id,
                    Name = $"{x.FirstName} {x.LastName}  {x.MiddleName}",
                    NameGroup = x.Group.Name != null ? x.Group.Name : " - ",
                    Email = x.User.Email,
                    DayOfbirthday = x.DayOfbirthday,
                    Image = x.Image != null ? x.Image : " - ",
                    PhoneNumber = x.User.PhoneNumber,
                    GroupId = x.GroupId.Value
                }
            ).ToListAsync();
            // var listStudent =
            //     users.Join(students, 
            //         u => u.Id, 
            //         s => s.Id, 
            //         (u, s) =>new StudentVM()
            //         {
            //             Age = s.DayOfbirthday,Email = u.Email,Image = s.Image,Name = s.FirstName +" " +s.LastName + " " + s.MiddleName
            //        ,PhoneNumber = u.PhoneNumber,
            //             GroupId = s.GroupIdOf.Value
            //
            //         })
            // .Join(groups,
            //         s=>s.GroupId,
            //         g=>g.Id, 
            //         (u, s) =>new StudentVM()
            //         {
            //             Age = u.DayOfbirthday,Email = u.Email,Image = u.Image,Name = u.Name,PhoneNumber = u.PhoneNumber,
            //             GroupId = u.GroupId,
            //             Group = new GroupVM()
            //             {
            //                 Name = s.Name,
            //                 Id = u.GroupId
            //             }
            //
            //         }).ToList();
            return list;
        }


        public async Task<List<StudentVM>> GetStudents()
        {
            //        var Id = _context.Filters.Where(p => p.CarId == CarId).Select(p => new Filter
            //   {
            //       FilterValueId = p.FilterValueId
            //  });


            // var listStudent =
            //     users.Join(students, 
            //         u => u.Id, 
            //         s => s.Id, 
            //         (u, s) =>new StudentVM()
            //         {
            //             Age = s.DayOfbirthday,Email = u.Email,Image = s.Image,FirstName = s.FirstName
            //             ,LastName = s.LastName,MiddleName = s.MiddleName,PhoneNumber = u.PhoneNumber,
            //             GroupId = s.GroupIdOf.Value
            //
            //         }).Join(groups,
            //       s=>s.GroupId,
            //       g=>g.Id, 
            //       (u, s) =>new StudentVM()
            //       {
            //           Age = u.DayOfbirthday,Email = u.Email,Image = u.Image,FirstName = u.FirstName
            //           ,LastName = u.LastName,MiddleName = u.MiddleName,PhoneNumber = u.PhoneNumber,
            //           GroupId = u.GroupId,
            //           Group = new GroupVM()
            //           {
            //               Name = s.Name,
            //               Teacher = _context.Teachers.Where(st=>st.GroupIdOf==u.GroupId).Join(_context.Users, 
            //                   u => u.Id, 
            //                   s => s.Id, 
            //                   (s, u) =>new TeacherVM()
            //                   {
            //                       Age = s.DayOfbirthday,Email = u.Email,Image = s.Image,FirstName = s.FirstName
            //                       ,LastName = s.LastName,MiddleName = s.MiddleName,PhoneNumber = u.PhoneNumber,
            //                       Group = _context.Groups.Where(st=>st.TeacherIdOf==u.Id).Select(gr=>new GroupVM()
            //                       {
            //                           Name = gr.Name
            //                           
            //                       }).SingleOrDefault()
            //
            //                   }).SingleOrDefault(),
            //               Students = _context.Students.Where(st=>st.GroupIdOf==u.GroupId).Join(_context.Users, 
            //                   u => u.Id, 
            //                   s => s.Id, 
            //                   (s, u) =>new StudentVM()
            //                   {
            //                       Age = s.DayOfbirthday,Email = u.Email,Image = s.Image,FirstName = s.FirstName
            //                       ,LastName = s.LastName,MiddleName = s.MiddleName,PhoneNumber = u.PhoneNumber
            //
            //                   }).ToList()
            //           }
            //
            //       }).ToList();
            var students = _context.Students.AsQueryable();

            return await GetJoinStudents(students);
        }

        public async Task<List<StudentVM>> GetStudentsByName(string Name)
        {
            var students = _context.Students.Where(st => Name.Contains((st.FirstName + st.LastName + st.MiddleName)))
                .ToList();
            return null; //await GetJoinStudents(students); ;
        }



        public async Task<List<StudentVM>> GetStudentsByGroup(long GroupId)
        {
            var students = _context.Students.Where(st => st.GroupId == GroupId).AsQueryable();
            return await GetJoinStudents(students);
        }

        public async Task<List<StudentVM>> GetStudentsByTeacher(long TeacherId)
        {
            var students = _context.Students.Where(st => st.Group.TeacherId == TeacherId).AsQueryable();

            return await GetJoinStudents(students);
        }

        public StudentVM GetStudentById(long StudentId)
        {
            var student = _context.Students.Where(st => st.Id == StudentId).Select(student => new StudentVM
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                Name = $"{student.FirstName} {student.LastName}  {student.MiddleName}",
                NameGroup = student.Group.Name,
                Email = student.User.Email,
                DayOfbirthday = student.DayOfbirthday,
                Image = student.Image,
                PhoneNumber = student.User.PhoneNumber,
                GroupId = student.GroupId.Value,
                NameTeacher =
                    $"{student.Group.Teacher.FirstName} {student.Group.Teacher.LastName}  {student.Group.Teacher.MiddleName}",
                TeacherId = student.Group.TeacherId.Value,
                SchoolId = (int?)student.SchoolId
            }).SingleOrDefault();

            return student;
        }


        public async Task<bool> Create(StudentRegisterVM model)
        {

            if (_userManager.FindByEmailAsync(model.Email).Result == null)
            {
                var student = new Student()
                {
                    DayOfbirthday = model.DayOfbirthday,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    GroupId = model.GroupId
                };
                var user = new DbUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Student = student,
                };
                var result = _userManager.CreateAsync(user, "8Ki9x9-3of+s").Result;
                result = _userManager.AddToRoleAsync(user, "Student").Result;
                return result.Succeeded;
            }
            return false;
        }
        public async Task<List<StudentVM>> GetStudentsWithoutGroup()
        {
            return await _context.Students.Where((item) => item.GroupId == null).Select(student => new StudentVM()
            {
                Name = $"{student.FirstName} {student.LastName}  {student.MiddleName}",
                Email = student.User.Email,
                DayOfbirthday = student.DayOfbirthday,
                Image = student.Image,
                PhoneNumber = student.User.PhoneNumber,
                Id = student.Id
            }).ToListAsync();
        }
        public async Task<bool> DeleteStudents(int[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var student = _context.Students.Where((item) => item.Id == id).FirstOrDefault();

                    if (student != null)
                    {
                        var group = _context.Groups.FirstOrDefault((item) => item.TeacherId == student.Id);
                        if (group != null)
                        {
                            group.Students.Remove(student);
                        }

                        _context.Students.Remove(student);

                        await _context.SaveChangesAsync();

                        var user = _context.Users.FirstOrDefault((item) => item.Id == id);
                        if (user != null)
                        {
                            await _userManager.DeleteAsync(user);
                        }
                    }
                }
                return true;

            }
            catch (Exception e)
            {
                return false;

            }

        }

        public async Task<bool> CreateStudent(TeacherCreateVM model)
        {
            if (_userManager.FindByEmailAsync(model.Email).Result == null)
            {
                var student = new Student()
                {
                    DayOfbirthday = model.DayOfbirthday,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    SchoolId = model.SchoolId
                };
                var user = new DbUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Student = student
                };
                var result = _userManager.CreateAsync(user, "Qwerty1234").Result;
                result = _userManager.AddToRoleAsync(user, "Student").Result;
                return result.Succeeded;
            }
            return false;
        }
        public async Task<bool> UpdateStudent(TeacherCreateVM model)
        {
            var student = _context.Students.Where((item) => item.Id == model.Id).SingleOrDefault();
            if (student != null)
            {
                var newStudent = new Student()
                {
                    DayOfbirthday = model.DayOfbirthday,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName
                };

                student.DayOfbirthday = model.DayOfbirthday;
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.MiddleName = model.MiddleName;

                var user = await _userManager.FindByIdAsync(student.Id.ToString());
                if (user != null)
                {
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.UserName = model.Email;
                    var result = _userManager.UpdateAsync(user).Result;
                    return result.Succeeded;
                }
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<List<StudentVM>> GetStudentsBySchool(long schoolId)
        {
            var students = _context.Students.Where((item) => item.SchoolId == schoolId).AsQueryable();

            return await GetJoinStudents(students);
        }
    }
}