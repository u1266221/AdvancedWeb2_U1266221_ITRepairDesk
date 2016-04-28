using System.Collections.Generic;
using ITRepairDeskWebApp.Models;

namespace ITRepairDeskWebApp.ViewModels
{
    public class ClientIndexData
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<JobAssignment> JobAssignments { get; set; }
    }
}