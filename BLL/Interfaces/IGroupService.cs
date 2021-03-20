using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models.GroupsModels;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IGroupService
    {
        Task<bool> AddStudentToGroup(long GroupId, Student student);
        Task<List<GroupVM>> GetGroups();
        Task<bool> CreateGroup(UpdateAndCreateGroupVM model);
        Task<bool> DeleteGroup(int id);
        Task<bool> EditGroup(UpdateAndCreateGroupVM model);

        Task<List<GroupVM>> GetGroupsBySchool(long schoolId);
    }
}