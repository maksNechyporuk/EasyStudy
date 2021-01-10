using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IGroupService
    {
        Task<bool> AddStudentToGroup(long GroupId, Student student);
    }
}