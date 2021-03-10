using BLL.Interfaces;
using BLL.Models.GroupsModels;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GroupService : IGroupService
    {

        private readonly EFContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;

        public GroupService(EFContext context,
            IConfiguration configuration,
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> AddStudentToGroup(long GroupId, Student model)
        {
            var student = _context.Students.SingleOrDefault((item) => item.Id == model.Id);
            if (student != null)
            {
                student.GroupId = GroupId;
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
        public async Task<bool> CreateGroup(UpdateAndCreateGroupVM model)
        {
            var group = _context.Groups.Where((item) => item.Name == model.Name).FirstOrDefault();
            if (group is null)
            {
                group = new Group
                {
                    Name = model.Name,
                    TeacherId = model.TeacherId
                };

                _context.Groups.Add(group);
                await _context.SaveChangesAsync();
                var teacher = _context.Teachers.FirstOrDefault((item) => item.Id == model.TeacherId);
                if (teacher != null)
                {
                    teacher.GroupId = group.Id;
                    await _context.SaveChangesAsync();
                }
                foreach (var id in model.StudentsId)
                {
                    var student = _context.Students.SingleOrDefault((item) => item.Id == id);
                    if (student != null)
                    {
                        student.GroupId = group.Id;
                    }
                }
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> DeleteGroup(int id)
        {
            var group = _context.Groups.Where((item) => item.Id == id).FirstOrDefault();
            if (group != null)
            {
                _context.Groups.Remove(group);
                var students = _context.Students.Where((item) => item.GroupId == id).ToList();
                if (students.Count > 0)
                {
                    foreach (var item in students)
                    {
                        item.GroupId = null;
                    }
                }
                var teacher = _context.Teachers.SingleOrDefault((item) => item.GroupId == id);
                if (teacher != null)
                {
                    teacher.GroupId = null;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditGroup(UpdateAndCreateGroupVM model)
        {
            var group = _context.Groups.Where((item) => item.Id == model.Id).FirstOrDefault();
            if (group != null)
            {
                if (group.TeacherId != null)
                {
                    var teacherOld = _context.Teachers.FirstOrDefault((item) => item.Id == group.TeacherId);
                    if (teacherOld != null)
                    {
                        teacherOld.GroupId = null;
                    }
                }

                var teacher = _context.Teachers.FirstOrDefault((item) => item.Id == model.TeacherId);
                if (teacher != null)
                {
                    teacher.GroupId = group.Id;
                }

                var oldStudents = _context.Students.Where((item) => item.GroupId == group.Id).ToList();
                foreach (var student in oldStudents)
                {
                    student.GroupId = null;
                }

                foreach (var id in model.StudentsId)
                {
                    var student = _context.Students.SingleOrDefault((item) => item.Id == id);
                    if (student != null)
                    {
                        student.GroupId = group.Id;
                    }
                }
                group.Name = model.Name;
                group.TeacherId = model.TeacherId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<GroupVM>> GetGroups()
        {
            var list = _context.Groups.Select((item) => new GroupVM
            {
                Id = item.Id,
                Name = item.Name,
                TeacherName = $"{item.Teacher.FirstName} {item.Teacher.LastName} {item.Teacher.MiddleName}",
                QuantityOfStudents = item.Students.Count
            }).ToListAsync();

            return await list;
        }
    }
}
