using System;
using System.Collections.Generic;

namespace LeaveRequestApp.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime? TimeStamp { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
