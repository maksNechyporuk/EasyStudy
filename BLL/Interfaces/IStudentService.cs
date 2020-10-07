using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentVM>> GetJoinStudents(IQueryable<Student> students);
        Task<List<StudentVM>> GetStudents();
        Task<List<StudentVM>> GetStudentsByName(string Name);
        Task<List<StudentVM>> GetStudentsByAge(int a, int b);
        Task<List<StudentVM>> GetStudentsByGroup(long GroupId);
        Task<List<StudentVM>> GetStudentsByTeacher(long TeacherId);
        Task<StudentVM> GetStudentById(long StudentId);
        Task<bool> Create(StudentVM student);
    }
}