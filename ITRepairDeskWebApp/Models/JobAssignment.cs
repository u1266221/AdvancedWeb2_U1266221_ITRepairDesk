using System.Collections.Generic;

namespace ITRepairDeskWebApp.Models
{
    public class JobAssignment
    {
        public int JobAssignmentID { get; set; }
        public int JobID { get; set; }
        public int TechnicianID { get; set; }

        public virtual Job Job { get; set; }
        public virtual Technician Technician { get; set; }


    }
} 