using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.TeacherModels
{
    public class TeacherCreateVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime DayOfbirthday { get; set; }
        public long? GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
