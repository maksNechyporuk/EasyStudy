using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyStudy.BLL.Interfaces;
using EasyStudy.BLL.Models;
using EasyStudy.BLL.Models.TeacherModels;
using EasyStudy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EasyStudy.BLL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly EFContext _context;
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentService;

        public TeacherService(EFContext context,
            IConfiguration configuration,IStudentService studentService)
        {
            _context = context;
            _configuration = configuration;
            _studentService = studentService;
        }
        public async Task<List<TeacherVM>> GetJoinTeachers(IQueryable<Teacher> teachers)
        {
            return await teachers.Select(x =>
                new TeacherVM()
                {
                    Name = $"{x.FirstName} {x.LastName}  {x.MiddleName}",
                    NameGroup = x.Group.Name,
                    Email = x.User.Email,
                    Age = x.Age,
                    Image = x.Image,
                    PhoneNumber = x.User.PhoneNumber,
                    GroupId = x.GroupId.Value,
                    Students = x.Group.Students.Select(st => new StudentVM()
                    {
                        Age = st.Age,
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

        public async Task<List<TeacherVM>> GetTeachersByAge(int a, int b)
        {
            var teachers = _context.Teachers.Where(t => t.Age >= a && t.Age <= b).AsQueryable();

            return await GetJoinTeachers(teachers);
        }

        public async Task<TeacherVM> GetTeacherByGroup(long GroupId)
        {
            var teacher = _context.Teachers.Where(t => t.GroupId==GroupId).Select(x =>
                new TeacherVM()
                {
                    Name = $"{x.FirstName} {x.LastName}  {x.MiddleName}",
                    NameGroup = x.Group.Name,
                    Email = x.User.Email,
                    Age = x.Age,
                    Image = x.Image,
                    PhoneNumber = x.User.PhoneNumber,
                    GroupId = x.GroupId.Value,
                    Students = x.Group.Students.Select(st => new StudentVM()
                        {
                            Age = st.Age,
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
            ).SingleOrDefaultAsync(); 

            return await teacher;
        }

        public async Task<TeacherVM> GetTeachersByStudent(long StudentId)
        {

            //     st => st.Group.Students.Where(st=>st.Id==StudentId).Select(
            //         st=> new StudentVM()
            //         {
            //             Age = st.Age,
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
            var student= await _studentService.GetStudentById(StudentId);

            var teacher = await GetTeacherById(student.TeacherId);
            
            // var teacher = _context.Teachers.GroupJoin(_context.Students,
            //     t => t.Id,
            //     st => st.Group.TeacherId,
            //     (s, st) => new TeacherVM()
            //     {
            //         Age = s.Age,
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
                Age = student.Age,
                Image = student.Image,
                PhoneNumber = student.User.PhoneNumber,
                GroupId = student.GroupId.Value,
                Students = student.Group.Students.Select(st => new StudentVM()
                {
                    Age = st.Age,
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
    }
}