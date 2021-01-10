using System;
using System.ComponentModel.DataAnnotations;
using BLL.Models.AccountModels;

namespace BLL.Models
{
    public class StudentRegisterVM : AccountVM
    {
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public DateTime DayOfbirthday { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public long GroupId { get; set; }
    }
}