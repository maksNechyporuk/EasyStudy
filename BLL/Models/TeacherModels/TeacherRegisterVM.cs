using System;
using System.ComponentModel.DataAnnotations;
using BLL.Models.AccountModels;

namespace BLL.Models.TeacherModels
{
    public class TeacherRegisterVM : AccountVM
    {
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string MiddleName { get; set; }

        public DateTime DayOfbirthday { get; set; }

        public string Region { get; set; }

        public string School { get; set; }
        public string Photo { get;  set; }
    }
}