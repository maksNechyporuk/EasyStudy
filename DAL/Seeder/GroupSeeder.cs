using System.IO;
using System.Linq;
using DAL.Entities;

namespace DAL.Seeder
{
    public class GroupSeeder
    {
        public static void SeedGroups(EFContext context)
        {

            string name = "1";
            if (context.Groups.SingleOrDefault(c => c.Name == name) == null)
            {
                Group group = new Group
                {
                    Name = name,
                    TeacherId = 1,
                    SchoolId = 1

                };
                context.Groups.Add(group);
                context.SaveChanges();
            }
            name = "2";
            if (context.Groups.SingleOrDefault(c => c.Name == name) == null)
            {
                Group group = new Group
                {
                    Name = name,
                    TeacherId = 2,
                    SchoolId = 1
                };
                context.Groups.Add(group);
                context.SaveChanges();
            }
        }
    }
}