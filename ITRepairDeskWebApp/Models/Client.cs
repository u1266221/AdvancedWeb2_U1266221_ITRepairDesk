using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITRepairDeskWebApp.Models
{
    public class Client
    {
        public int ClientID { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string ExtNo { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}