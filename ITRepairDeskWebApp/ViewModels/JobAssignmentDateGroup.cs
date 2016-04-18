using System;
using System.ComponentModel.DataAnnotations;

namespace ITRepairDeskWebApp.ViewModels
{
    public class JobAssignmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? JobAssignDate { get; set; }

        public int TechnicianCount { get; set; }
    }
}