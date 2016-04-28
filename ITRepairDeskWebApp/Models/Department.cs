using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITRepairDeskWebApp.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public int? ClientID { get; set; }

        public virtual Client Administrator { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}