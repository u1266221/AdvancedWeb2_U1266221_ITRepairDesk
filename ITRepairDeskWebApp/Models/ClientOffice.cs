using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITRepairDeskWebApp.Models
{
    public class ClientOffice
    {
            [Key]
            [ForeignKey("Client")]
            public int ClientID { get; set; }

            [StringLength(50)]
            [Display(Name = "Office Location")]
            public string Location { get; set; }

            public virtual Client Client { get; set; }
        }
    }