using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.Models;
using BLL.Models.TeacherModels;

namespace BLL.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentVM>> GetJoinStudents(IQueryable<Student> students);
        Task<List<StudentVM>> GetStudents();
        Task<List<StudentVM>> GetStudentsWithoutGroup();
        Task<List<StudentVM>> GetStudentsByName(string Name);
        Task<List<StudentVM>> GetStudentsByAge(int a, int b);
        Task<List<StudentVM>> GetStudentsByGroup(long GroupId);
        Task<List<StudentVM>> GetStudentsByTeacher(long TeacherId);
        StudentVM GetStudentById(long StudentId);
        Task<bool> Create(StudentRegisterVM student);
        Task<bool> CreateStudent(TeacherCreateVM model);
        Task<bool> DeleteStudents(int[] ids);
        Task<bool> UpdateStudent(TeacherCreateVM model);
        Task<List<StudentVM>> GetStudentsBySchool(long groupId);

    }
}