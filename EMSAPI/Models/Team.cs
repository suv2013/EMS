using System;
using System.Collections.Generic;

namespace LeaveRequestApp.Models
{
    public partial class Team
    {
        public Team()
        {
            UserTeam = new HashSet<UserTeam>();
        }

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime? TimeStamp { get; set; }

        public virtual ICollection<UserTeam> UserTeam { get; set; }
    }
}
