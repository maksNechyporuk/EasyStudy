using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.Models.TeacherModels;
using System;

namespace BLL.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherVM>> GetJoinTeachers(IQueryable<Teacher> teachers);
        Task<List<TeacherVM>> GetTeachers();
        Task<bool> CreateTeacher(TeacherCreateVM model);

        Task<bool> UpdateTeacher(TeacherCreateVM model);
        Task<List<TeacherVM>> GetTeachersByName(string Name);
        Task<List<TeacherVM>> GetTeachersByAge(DateTime a, DateTime b);
        Task<TeacherVM> GetTeacherByGroup(long GroupId);
        Task<TeacherVM> GetTeachersByStudent(long StudentId);
        Task<TeacherVM> GetTeacherById(long StudentId);
        Task<bool> Create(TeacherRegisterVM student);
        Task<List<TeacherVM>> GetTeachersWithoutGroup();
        Task<bool> DeleteTeacher(int[] ids);
        Task<List<TeacherVM>> GetTeachersBySchool(long groupId);

    }
}