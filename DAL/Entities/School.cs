using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class School
    {
        [Key]
        public long Id { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public string Region { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

    }
}
