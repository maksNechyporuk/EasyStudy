using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.TeacherModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Diagnostics;

namespace BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly EFContext _context;
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentService;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly IGroupService _groupService;
        public TeacherService(EFContext context,
            IConfiguration configuration, IStudentService studentService,
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager, IGroupService groupService)
        {
            _context = context;
            _configuration = configuration;
            _studentService = studentService;
            _userManager = userManager;
            _signInManager = signInManager;
            _groupService = groupService;
        }
        public async Task<List<TeacherVM>> GetJoinTeachers(IQueryable<Teacher> teachers)
        {
            //\Uploaded\Users\600_ofp5tum0.igd.jpg
            var list = await teachers.Select(x =>
             new TeacherVM()
             {
                 Id = x.Id,
                 Name = $"{x.FirstName} {x.LastName}  {x.MiddleName}",
                 NameGroup = x.Group.Name != null ? x.Group.Name : " - ",
                 Email = x.User.Email,
                 DayOfbirthday = x.DayOfbirthday,
                 Image = x.Image.Contains("cloudflare") || x.Image.Contains("randomuser") ? x.Image : @"Uploaded\Users\600_" + x.Image,
                 PhoneNumber = x.User.PhoneNumber,
                 GroupId = x.GroupId.Value,
                 Students = x.Group.Students.Select(st => new StudentVM()
                 {
                     Id = st.Id,
                     DayOfbirthday = st.DayOfbirthday,
                     Email = st.User.Email,
                     Image = st.Image,
                     Name = $"{st.FirstName} {st.LastName}  {st.MiddleName}",
                     GroupId = st.GroupId.Value,
                     NameGroup = st.Group.Name,
                     NameTeacher = $"{st.Group.Teacher.FirstName} {st.Group.Teacher.LastName} {st.Group.Teacher.MiddleName}",
                     PhoneNumber = st.User.PhoneNumber,
                     TeacherId = st.Group.TeacherId.Value
                 }
                 ).ToList()
             }
         ).ToListAsync();
            return list;
        }

        public async Task<List<TeacherVM>> GetTeachers()
        {
            var teachers = _context.Teachers.AsQueryable();
            return await GetJoinTeachers(teachers);
        }

        public async Task<List<TeacherVM>> GetTeachersByName(string Name)
        {
            var teachers = _context.Teachers.Where(st => (st.FirstName + st.LastName + st.MiddleName).Contains(Name)).AsQueryable();
            return await GetJoinTeachers(teachers); ;
        }

        public async Task<List<TeacherVM>> GetTeachersByAge(DateTime a, DateTime b)
        {
            var teachers = _context.Teachers.Where(t => t.DayOfbirthday >= a && t.DayOfbirthday <= b).AsQueryable();

            return await GetJoinTeachers(teachers);
        }

        public async Task<TeacherVM> GetTeacherByGroup(long GroupId)
        {
            var teacher = _context.Teachers.Where(t => t.GroupId == GroupId).Select(x =>
                  new TeacherVM()
                  {
                      Id = x.Id,
                      Name = $"{x.FirstName} {x.LastName}  {x.MiddleName}",
                      NameGroup = x.Group.Name,
                      Email = x.User.Email,
                      DayOfbirthday = x.DayOfbirthday,
                      Image = x.Image,
                      PhoneNumber = x.User.PhoneNumber,
                      GroupId = x.GroupId.Value
                  }
            ).FirstOrDefault();

            return teacher;
        }

        public async Task<TeacherVM> GetTeachersByStudent(long StudentId)
        {

            //     st => st.Group.Students.Where(st=>st.Id==StudentId).Select(
            //         st=> new StudentVM()
            //         {
            //             Age = st.DayOfbirthday,
            //             Email = st.User.Email,
            //             Image = st.Image,
            //             Name = $"{st.FirstName} {st.LastName}  {st.MiddleName}",
            //             GroupId = st.GroupId.Value,
            //             NameGroup = st.Group.Name,
            //             NameTeacher =  $"{st.Group.Teacher.FirstName} {st.Group.Teacher.LastName}  {st.Group.Teacher.MiddleName}",
            //             PhoneNumber = st.User.PhoneNumber,
            //             TeacherId = st.Group.TeacherId.Value
            //         }
            //     ).ToList().Count>0 )
            var student = await _studentService.GetStudentById(StudentId);

            var teacher = await GetTeacherById(student.TeacherId);

            // var teacher = _context.Teachers.GroupJoin(_context.Students,
            //     t => t.Id,
            //     st => st.Group.TeacherId,
            //     (s, st) => new TeacherVM()
            //     {
            //         Age = s.DayOfbirthday,
            //         Image = s.Image,
            //         GroupId = s.GroupId.Value,
            //     }).ToList();



            return teacher;
        }

        public async Task<TeacherVM> GetTeacherById(long TeacherId)
        {
            var teacher = _context.Teachers.Where(st => st.Id == TeacherId).Select(teacher => new TeacherVM()
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                Id = teacher.Id,
                Name = $"{teacher.FirstName} {teacher.MiddleName} {teacher.LastName}",
                NameGroup = teacher.Group.Name,
                Email = teacher.User.Email,
                DayOfbirthday = teacher.DayOfbirthday,
                Image = teacher.Image,
                PhoneNumber = teacher.User.PhoneNumber,
                GroupId = teacher.GroupId.Value,
                SchoolId = (int)teacher.SchoolId,
                Students = teacher.Group.Students.Select(st => new StudentVM()
                {
                    DayOfbirthday = st.DayOfbirthday,
                    Email = st.User.Email,
                    Image = st.Image,
                    Name = $"{st.FirstName} {st.LastName}  {st.MiddleName}",
                    GroupId = st.GroupId.Value,
                    NameGroup = st.Group.Name,
                    NameTeacher = $"{st.Group.Teacher.FirstName} {st.Group.Teacher.LastName}  {st.Group.Teacher.MiddleName}",
                    PhoneNumber = st.User.PhoneNumber,
                    TeacherId = st.Group.TeacherId.Value
                }
              ).ToList()
            }).SingleOrDefaultAsync();


            return await teacher;
        }

        public async Task<bool> Create(TeacherRegisterVM model)
        {

            if (_userManager.FindByEmailAsync(model.Email).Result == null)
            {
                try
                {
                    var school = _context.Schools.SingleOrDefault((item) => item.Name == model.School);
                    if (school == null)
                    {
                        var newSchool = new School
                        {
                            Name = model.School,
                            Region = model.Region
                        };
                        _context.Schools.Add(newSchool);
                        _context.SaveChanges();
                        var teacher = new Teacher()
                        {
                            DayOfbirthday = model.DayOfbirthday,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            MiddleName = model.MiddleName,
                            SchoolId = newSchool.Id,
                            Image = model.Photo
                        };
                        var user = new DbUser()
                        {
                            Email = model.Email,
                            UserName = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            Teacher = teacher
                        };
                        var result = _userManager.CreateAsync(user, model.Password).Result;
                        result = _userManager.AddToRoleAsync(user, "Admin").Result;
                        return result.Succeeded;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

            }
            return false;

        }

        public async Task<List<TeacherVM>> GetTeachersWithoutGroup()
        {
            return await _context.Teachers.Where((item) => item.GroupId == null).Select(teacher => new TeacherVM()
            {
                Id = teacher.Id,
                Name = $"{teacher.FirstName} {teacher.LastName}  {teacher.MiddleName}",
                Email = teacher.User.Email,
                DayOfbirthday = teacher.DayOfbirthday,
                Image = teacher.Image,
                PhoneNumber = teacher.User.PhoneNumber,
            }).ToListAsync();
        }

        public async Task<bool> DeleteTeacher(int[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var teacher = _context.Teachers.Where((item) => item.Id == id).FirstOrDefault();

                    if (teacher != null)
                    {
                        var group = _context.Groups.FirstOrDefault((item) => item.TeacherId == teacher.Id);
                        if (group != null)
                        {
                            await _groupService.DeleteGroup((int)group.Id);
                        }


                        _context.Teachers.Remove(teacher);

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

        public async Task<bool> CreateTeacher(TeacherCreateVM model)
        {
            if (_userManager.FindByEmailAsync(model.Email).Result == null)
            {
                var teacher = new Teacher()
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
                    Teacher = teacher
                };
                var result = _userManager.CreateAsync(user, "Qwerty1234").Result;
                result = _userManager.AddToRoleAsync(user, "Teacher").Result;
                return result.Succeeded;
            }
            return false;
        }
        public async Task<bool> UpdateTeacher(TeacherCreateVM model)
        {
            var teacher = _context.Teachers.Where((item) => item.Id == model.Id).SingleOrDefault();
            if (teacher != null)
            {
                var newTeacher = new Teacher()
                {
                    DayOfbirthday = model.DayOfbirthday,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName
                };

                teacher.DayOfbirthday = model.DayOfbirthday;
                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;
                teacher.MiddleName = model.MiddleName;

                var user = await _userManager.FindByIdAsync(teacher.Id.ToString());
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

        public async Task<List<TeacherVM>> GetTeachersBySchool(long schoolId)
        {
            var teachers = _context.Teachers.Where((item) => item.SchoolId == schoolId).AsQueryable();
            return await GetJoinTeachers(teachers);
        }
    }
}