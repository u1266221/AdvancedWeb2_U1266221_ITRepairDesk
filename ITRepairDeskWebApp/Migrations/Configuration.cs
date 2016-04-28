namespace ITRepairDeskWebApp.Migrations
{ 
using ITRepairDeskWebApp.DAL;
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
                    JobAssignDate = DateTime.Parse("2012-03-02"), Email="Jozell@email.com", ContactNo="2836" },

                new Technician { FirstMidName = "Rebecca",      LastName = "Jayne",
                    JobAssignDate = DateTime.Parse("2011-06-09"), Email="Rebecca@email.com", ContactNo="4784" }

            };
            technicians.ForEach(s => context.Technicians.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();


            var clients = new List<Client>
            {
                new Client { FirstName = "Kim", LastName = "Abercrombie", Email= "Kim@email.com",
                    ExtNo ="1234",},
               new Client { FirstName = "Madison", LastName = "Crampton", Email= "Madison@email.com",
                    ExtNo ="4543",},
               new Client { FirstName = "Carl", LastName = "Noble", Email= "Carl@email.com",
                    ExtNo ="9749",},
            };
            clients.ForEach(s => context.Clients.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "Physiotheraphy",
                    ClientID  = clients.Single( i => i.LastName == "Abercrombie").ClientID },
                new Department { Name = "X-ray",
                    ClientID  = clients.Single( i => i.LastName == "Crampton").ClientID },
                new Department { Name = "Ward 31",
                    ClientID  = clients.Single( i => i.LastName == "Noble").ClientID },
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();



            var jobs = new List<Job>
            {
                new Job {JobID = 1050, Title = "Mouse", Detail="Mouse need replacing",
                    Status =Status.New,Priority=Priority.Medium,
                    DepartmentID = departments.Single( s => s.Name == "Physiotheraphy").DepartmentID,
                    Clients = new List<Client>() },

                new Job {JobID = 1230, Title = "Keyboard", Detail="Need replacing",
                    Status =Status.Open,Priority=Priority.High,
                    DepartmentID = departments.Single( s => s.Name == "X-ray").DepartmentID,
                    Clients = new List<Client>() },

                new Job {JobID = 2341, Title = "Laptop", Detail="Need replacing",
                    Status =Status.WaitingResponse,Priority=Priority.Critical,
                    DepartmentID = departments.Single( s => s.Name == "Ward 31").DepartmentID,
                    Clients = new List<Client>() },
            };
            jobs.ForEach(s => context.Jobs.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();


            var clientOffice = new List<ClientOffice>
            {
                new ClientOffice {
                    ClientID = clients.Single( i => i.LastName == "Abercrombie").ClientID,
                    Location = "Physiotheraphy" },
                new ClientOffice {
                    ClientID = clients.Single( i => i.LastName == "Crampton").ClientID,
                    Location = "X-ray" },
                new ClientOffice {
                    ClientID = clients.Single( i => i.LastName == "Noble").ClientID,
                    Location = "Ward 31" },
            };
            clientOffice.ForEach(s => context.ClientOffices.AddOrUpdate(p => p.ClientID, s));
            context.SaveChanges();

            AddOrUpdateClient(context, "Mouse", "Mouse need replacing",Status.New,Priority.Medium,"Abercrombie");
            AddOrUpdateClient(context, "Keyboard","Need replacing", Status.Open, Priority.High, "Crampton");
            AddOrUpdateClient(context, "Laptop", "Need replacing", Status.WaitingResponse, Priority.Critical, "Noble");



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
                    JobID = jobs.Single(c => c.Title == "Laptop" ).JobID,
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
        void AddOrUpdateClient(ITRepairDeskWebAppContext context, string jobTitle, string jobDetail,
          Status? jobStatus, Priority? jobPriority, string  clientName)
        {
            var jbs = context.Jobs.SingleOrDefault(c => c.Title == jobTitle && c.Detail == jobDetail 
                     && c.Status == jobStatus && c.Priority == jobPriority);
            var cli = jbs.Clients.SingleOrDefault(i => i.LastName == clientName);
            if (cli == null)
                jbs.Clients.Add(context.Clients.Single(i => i.LastName == clientName));
        }
    }
}