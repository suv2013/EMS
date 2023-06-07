using System;
using System.Collections.Generic;

namespace LeaveRequestApp.Models
{
    public partial class UserTeam
    {
        public int UserTeamId { get; set; }
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
