namespace ITRepairDeskWebApp.Migrations
{
    using ITRepairDeskWebApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ITRepairDeskWebApp.DAL.ITRepairDeskWebAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ITRepairDeskWebApp.DAL.ITRepairDeskWebAppContext context)
        {


            var technicians = new List<Technician>
            {

                new Technician { FirstMidName = "James",   LastName = "Moffatt",
                    JobAssignDate = DateTime.Parse("2013-09-01"), Email="James@email.com", ContactNo="2341"},

                new Technician { FirstMidName = "Jozell",    LastName = "Resurreccion",
                    JobAssignDate = DateTime.Parse("2012-09-01"), Email="Jozell@email.com", ContactNo="2836" },

                new Technician { FirstMidName = "Rebecca",      LastName = "Jayne",
                    JobAssignDate = DateTime.Parse("2012-09-01"), Email="Rebecca@email.com", ContactNo="4784" }

            };
            technicians.ForEach(s => context.Technicians.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();




            var jobs = new List<Job>
            {
                new Job {JobID = 1050, Title = "Mouse", Detail="Mouse need replacing",
                    Status =Status.New,Priority=Priority.Critical  },

                new Job {JobID = 1050, Title = "Keyboard", Detail="Need replacing",
                    Status =Status.New,Priority=Priority.Critical  },

                new Job {JobID = 1050, Title = "Laptop", Detail="Need replacing",
                    Status =Status.New,Priority=Priority.Critical  },


            };
            jobs.ForEach(s => context.Jobs.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();


            var jobassignments = new List<JobAssignment>
            {
                 new JobAssignment {
                    TechnicianID = technicians.Single(s => s.LastName == "Moffatt").TechnicianID,
                    JobID = jobs.Single(c => c.Title == "Mouse" ).JobID,

                },

                 new JobAssignment {
                    TechnicianID = technicians.Single(s => s.LastName == "Resurreccion").TechnicianID,
                    JobID = jobs.Single(c => c.Title == "Keyboard" ).JobID,

                },
                  new JobAssignment {
                    TechnicianID = technicians.Single(s => s.LastName == "Jayne").TechnicianID,
                    JobID = jobs.Single(c => c.Title == "Laptop" ).JobID

                }
            };

            foreach (JobAssignment e in jobassignments)
            {
                var jobassignmentInDataBase = context.JobAssignments.Where(
                    s =>
                         s.Technician.TechnicianID == e.TechnicianID &&
                         s.Job.JobID == e.JobID).SingleOrDefault();

                if (jobassignmentInDataBase == null)
                {
                    context.JobAssignments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}