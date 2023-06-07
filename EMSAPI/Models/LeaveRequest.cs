using System;
using System.Collections.Generic;

namespace LeaveRequestApp.Models
{
    public partial class LeaveRequest
    {
        public LeaveRequest()
        {
            LeaveRequestApproval = new HashSet<LeaveRequestApproval>();
        }

        public int LeaveRequestId { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string RequestDetails { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<LeaveRequestApproval> LeaveRequestApproval { get; set; }
    }
}
