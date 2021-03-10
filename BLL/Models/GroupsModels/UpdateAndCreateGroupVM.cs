using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.GroupsModels
{
    public class UpdateAndCreateGroupVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int[] StudentsId { get; set; }
    }
}
