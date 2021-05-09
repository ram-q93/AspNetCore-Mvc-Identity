using System;
using System.Collections.Generic;

namespace CustomJwtApp.DataAccess.Entities
{
    public class Role
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

        public static implicit operator Role(Role v)
        {
            throw new NotImplementedException();
        }
    }
}