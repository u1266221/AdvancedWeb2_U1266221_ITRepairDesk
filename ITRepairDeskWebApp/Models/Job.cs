using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITRepairDeskWebApp.Models
{
    public enum Status
    {
        New, Open, Closed, Resolved, WaitingResponse, OnHold
    }
    public enum Priority
    {
        Low, Medium, High, Critical
    }

    public class Job
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JobID { get; set; }
        public string Title { get; set; }
        public Status? Status { get; set; }
        public Priority? Priority { get; set; }

        public int ClientID { get; set; }

        public virtual ICollection<JobAssignment> JobAssignments { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}