using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyStudy.BLL.Models.TeacherModels;
using EasyStudy.DAL.Entities;

namespace EasyStudy.BLL.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherVM>> GetJoinTeachers(IQueryable<Teacher> teachers);
        Task<List<TeacherVM>> GetTeachers();
        Task<List<TeacherVM>> GetTeachersByName(string Name);
        Task<List<TeacherVM>> GetTeachersByAge(int a, int b);
        Task<TeacherVM> GetTeacherByGroup(long GroupId);
        Task<TeacherVM> GetTeachersByStudent(long StudentId);
        Task<TeacherVM> GetTeacherById(long StudentId);
    }
}