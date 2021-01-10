using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{

    public class Group
    {
        [Key]
        public long Id { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public string Name { get;  set; }

        [ForeignKey("TeacherIdOf")]
        public long? TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }

    }
}
