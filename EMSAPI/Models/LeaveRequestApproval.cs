using System;
using System.Collections.Generic;

namespace LeaveRequestApp.Models
{
    public partial class LeaveRequestApproval
    {
        public int LeaveRequestApprovalId { get; set; }
        public int LeaveRequestId { get; set; }
        public int ApproverId { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual User Approver { get; set; }
        public virtual LeaveRequest LeaveRequest { get; set; }
    }
}
