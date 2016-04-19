using ITRepairDeskWebApp.Models;
using System;
using System.Collections.Generic;

namespace ITRepairDeskWebApp.Models
{
    public class Technician
    {
        public int TechnicianID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime JobAssignDate { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }

        public virtual ICollection<JobAssignment> JobAssignments { get; set; }
       
    }
}