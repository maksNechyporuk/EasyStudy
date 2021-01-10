using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("tblTeacher")]
    public class Teacher
    {
        [Key, ForeignKey("User")]
        public long Id { get; set; }
        [StringLength(255)]
        public string Image { get; set; }

        [Range(0, 130, ErrorMessage = "Error")]
        public DateTime DayOfbirthday { get; set; }

        [ForeignKey("GroupIdOf")]
        public long? GroupId { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string MiddleName { get; set; }
        public virtual Group Group { get; set; }
        public virtual DbUser User { get; set; }

    }
}
