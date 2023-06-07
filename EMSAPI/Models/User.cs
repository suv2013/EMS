using System;
using System.Collections.Generic;

namespace LeaveRequestApp.Models
{
    public partial class User
    {
        public User()
        {
            LeaveRequest = new HashSet<LeaveRequest>();
            LeaveRequestApproval = new HashSet<LeaveRequestApproval>();
            UserRole = new HashSet<UserRole>();
            UserTeam = new HashSet<UserTeam>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }
        public DateTime? TimeStamp { get; set; }

        public virtual ICollection<LeaveRequest> LeaveRequest { get; set; }
        public virtual ICollection<LeaveRequestApproval> LeaveRequestApproval { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<UserTeam> UserTeam { get; set; }
    }
}
