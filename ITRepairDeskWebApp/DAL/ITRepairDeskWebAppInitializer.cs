using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITRepairDeskWebApp.Models;

namespace ITRepairDeskWebApp.DAL
{
    public class ITRepairDeskWebAppInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ITRepairDeskWebAppContext>
    {
        protected override void Seed(ITRepairDeskWebAppContext context)
        {
            var technicians= new List<Technician>
            {
            new Technician{FirstMidName="Carson",LastName="Alexander",JobAssignDate=DateTime.Parse("2005-09-01"), Email="test@test.com", ContactNo="1234"},
            new Technician{FirstMidName="Meredith",LastName="Alonso",JobAssignDate=DateTime.Parse("2002-09-01")},
            new Technician{FirstMidName="Arturo",LastName="Anand",JobAssignDate=DateTime.Parse("2003-09-01")},
            new Technician{FirstMidName="Gytis",LastName="Barzdukas",JobAssignDate=DateTime.Parse("2002-09-01")},
            new Technician{FirstMidName="Yan",LastName="Li",JobAssignDate=DateTime.Parse("2002-09-01")},
            new Technician{FirstMidName="Peggy",LastName="Justice",JobAssignDate=DateTime.Parse("2001-09-01")},
            new Technician{FirstMidName="Laura",LastName="Norman",JobAssignDate=DateTime.Parse("2003-09-01")},
            new Technician{FirstMidName="Nino",LastName="Olivetto",JobAssignDate=DateTime.Parse("2005-09-01")}
            };

            technicians.ForEach(s => context.Technicians.Add(s));
            context.SaveChanges();

            var jobs = new List<Job>
            {
            new Job{JobID=1050,Title="Mouse is broken", Detail="Needs Replacing", Status=Status.New, Priority=Priority.Critical},
            new Job{JobID=4022,Title="Microeconomics",},
            new Job{JobID=4041,Title="Macroeconomics",},
            new Job{JobID=1045,Title="Calculus",},
            new Job{JobID=3141,Title="Trigonometry",},
            new Job{JobID=2021,Title="Composition",},
            new Job{JobID=2042,Title="Literature",}
            };
            jobs.ForEach(s => context.Jobs.Add(s));
            context.SaveChanges();

            var jobassignment = new List<JobAssignment>
            {
            new JobAssignment{TechnicianID=1,JobID=1050,},
            new JobAssignment{TechnicianID=1,JobID=4022,},
            new JobAssignment{TechnicianID=1,JobID=4041,},
            new JobAssignment{TechnicianID=2,JobID=1045,},
            new JobAssignment{TechnicianID=2,JobID=3141,},
            new JobAssignment{TechnicianID=2,JobID=2021,},
            new JobAssignment{TechnicianID=3,JobID=1050},
            new JobAssignment{TechnicianID=4,JobID=1050,},
            new JobAssignment{TechnicianID=4,JobID=4022,},
            new JobAssignment{TechnicianID=5,JobID=4041,},
            new JobAssignment{TechnicianID=6,JobID=1045},
            new JobAssignment{TechnicianID=7,JobID=3141,},
            };
            jobassignment.ForEach(s => context.JobAssignments.Add(s));
            context.SaveChanges();
        }
    }
}