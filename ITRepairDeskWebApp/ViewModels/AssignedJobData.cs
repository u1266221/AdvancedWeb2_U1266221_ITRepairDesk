using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITRepairDeskWebApp.ViewModels
{
    public class AssignedJobData
    {
        public int JobID { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }
    }
}