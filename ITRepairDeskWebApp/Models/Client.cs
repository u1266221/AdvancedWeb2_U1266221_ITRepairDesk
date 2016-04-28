using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITRepairDeskWebApp.Models
{
    public class Client
    {
        public int ClientID { get; set;}

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string ExtNo { get; set; }


        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ClientOffice ClientOffice { get; set; }



    }
}