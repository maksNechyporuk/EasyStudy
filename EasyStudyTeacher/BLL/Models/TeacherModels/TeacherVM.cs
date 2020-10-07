using System.Collections.Generic;
using EasyStudy.BLL.Models.AccountModels;
using EasyStudy.BLL.Models.GroupsModels;

namespace EasyStudy.BLL.Models.TeacherModels
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