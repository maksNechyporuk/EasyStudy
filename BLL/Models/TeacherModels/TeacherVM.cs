using System.Collections.Generic;
using BLL.Models.AccountModels;
using BLL.Models.GroupsModels;

namespace BLL.Models.TeacherModels
{
    public class TeacherVM: AccountVM
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public double Age { get; set; }
        public string NameGroup{ get; set; }
        public long GroupId { get; set; }
        public List<StudentVM> Students{ get; set; }
    }
}