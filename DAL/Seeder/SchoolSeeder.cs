using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Seeder
{
    public class SchoolSeeder
    {
        public static void SeedSchools(EFContext context)
        {

            string name = "School 1";
            if (context.Schools.SingleOrDefault(c => c.Name == name) == null)
            {
                School school = new School
                {
                    Name = name,
                    Region = "City Rivne"
                };
                context.Schools.Add(school);
                context.SaveChanges();
            }
        }
    }
}
