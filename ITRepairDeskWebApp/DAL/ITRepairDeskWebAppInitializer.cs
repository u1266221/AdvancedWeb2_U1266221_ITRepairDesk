using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ITRepairDeskWebApp.Models;

namespace ITRepairDeskWebApp.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ITRepairDeskWebAppContext>
    {
        protected override void Seed(ITRepairDeskWebAppContext context)
        {
            var technicians = new List<Technician>
            {
            new Technician{FirstMidName="Carson",LastName="Alexander",JobAssignDate=DateTime.Parse("2005-09-01"),Email="test@test.com",ContactNo="1234",}
            };

            technicians.ForEach(s => context.Technicians.Add(s));
            context.SaveChanges();

            var jobs = new List<Job>
            {
            new Job{JobID=1050,Title="Mouse is not working",Status=Status.New,Priority=Priority.Critical,}

            };
            jobs.ForEach(s => context.Jobs.Add(s));
            context.SaveChanges();

            var jobassignments = new List<JobAssignment>
            {
            new JobAssignment{TechnicianID=1,JobID=1050,},
            };           
            jobassignments.ForEach(s => context.JobAssignments.Add(s));
            context.SaveChanges();
        }
    }
}