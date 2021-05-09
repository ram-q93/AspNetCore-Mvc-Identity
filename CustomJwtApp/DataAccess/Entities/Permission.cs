using System.Collections.Generic;

namespace CustomJwtApp.DataAccess.Entities
{
    public class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}