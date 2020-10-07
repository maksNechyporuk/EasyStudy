using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;


namespace DAL.Entities
{
    public class DbRole : IdentityRole<long>
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
    }
}
