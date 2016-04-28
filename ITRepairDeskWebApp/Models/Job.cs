using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Number")]
        public int JobID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500, MinimumLength = 3)]
        public string Detail { get; set; }

        public Status? Status { get; set; }
        public Priority? Priority { get; set; }

        public int DepartmentID { get; set; }


        public virtual Department Department { get; set; }
        public virtual ICollection<JobAssignment> JobAssignments { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}