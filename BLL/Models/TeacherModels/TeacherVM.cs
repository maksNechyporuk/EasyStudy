using System;
using System.Collections.Generic;
using BLL.Models.AccountModels;
using BLL.Models.GroupsModels;

namespace BLL.Models.TeacherModels
{
    public class TeacherVM : AccountVM
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DayOfbirthday { get; set; }
        public string NameGroup { get; set; }
        public long? GroupId { get; set; }
        public List<StudentVM> Students { get; set; }
        public int SchoolId { get; set; }

    }
}