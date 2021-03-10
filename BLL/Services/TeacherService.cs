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

namespace BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly EFContext _context;
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentService;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        public TeacherService(EFContext context,
            IConfiguration configuration, IStudentService studentService,
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager)
        {
            _context = context;
            _configuration = configuration;
            _studentService = studentService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<List<TeacherVM>> GetJoinTeachers(IQueryable<Teacher> teachers)
        {
            var list = await teachers.Select(x =>
                new TeacherVM()
                {
                    Id = x.Id,
                    Name = $"{x.FirstName} {x.LastName}  {x.MiddleName}",
                    NameGroup = x.Group.Name != null ? x.Group.Name : " - ",
                    Email = x.User.Email,
                    DayOfbirthday = x.DayOfbirthday,
                    Image = x.Image,
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
                        NameTeacher = $"{st.Group.Teacher.FirstName} {st.Group.Teacher.LastName}  {st.Group.Teacher.MiddleName}",
                        PhoneNumber = st.User.PhoneNumber,
                        TeacherId = st.Group.TeacherId.Value
                    }
                    ).ToList()
                }
            ).ToListAsync(); ;
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
            var teacher = _context.Teachers.Where(st => st.Id == TeacherId).Select(student => new TeacherVM()
            {
                Name = $"{student.FirstName} {student.LastName}  {student.MiddleName}",
                NameGroup = student.Group.Name,
                Email = student.User.Email,
                DayOfbirthday = student.DayOfbirthday,
                Image = student.Image,
                PhoneNumber = student.User.PhoneNumber,
                GroupId = student.GroupId.Value,
                Students = student.Group.Students.Select(st => new StudentVM()
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
                var teacher = new Teacher()
                {
                    DayOfbirthday = model.DayOfbirthday,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName
                };
                var user = new DbUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Teacher = teacher
                };
                var result = _userManager.CreateAsync(user, model.Password).Result;
                result = _userManager.AddToRoleAsync(user, "Teacher").Result;
                return result.Succeeded;
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
    }
}