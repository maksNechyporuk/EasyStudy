using System;
using System.ComponentModel.DataAnnotations;
using BLL.Models.AccountModels;
using BLL.Models.GroupsModels;

namespace BLL.Models
{
    public class StudentVM : AccountVM
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public DateTime DayOfbirthday { get; set; }
        public string NameGroup { get; set; }
        public long GroupId { get; set; }
        public string NameTeacher { get; set; }
        public long TeacherId { get; set; }
    }
}