using System.ComponentModel.DataAnnotations;

namespace BLL.Models.AccountModels
{
    public class AccountVM
    {
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [EmailAddress(ErrorMessage = "Не правильний формат електронної пошти!")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=\+?([0-9]{2})\s\(?([0-9]{3})\)\s?([0-9]{3})\s?([0-9]{2})\s?([0-9]{2})).{19}$", ErrorMessage = "Не правильний формат номера телефону!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string Password { get; set; }
    }
}