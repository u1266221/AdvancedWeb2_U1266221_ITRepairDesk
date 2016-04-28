using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITRepairDeskWebApp.DAL;
using ITRepairDeskWebApp.Models;
using ITRepairDeskWebApp.ViewModels;
using System.Data.Entity.Infrastructure;

namespace ITRepairDeskWebApp.Controllers
{
    public class ClientController : Controller
    {
        private ITRepairDeskWebAppContext db = new ITRepairDeskWebAppContext();

        // GET: View Client
        public ActionResult Index(int? id, int? jobID)
        {
            var viewModel = new ClientIndexData();
            viewModel.Clients = db.Clients
                .Include(i => i.ClientOffice)
                .Include(i => i.Jobs.Select(c => c.Department))
                .OrderBy(i => i.LastName);

            if (id != null)
            {
                ViewBag.ClientID = id.Value;
                viewModel.Jobs = viewModel.Clients.Where(
                    i => i.ClientID == id.Value).Single().Jobs;
            }

            if (jobID != null)
            {
                ViewBag.JobID = jobID.Value;
                viewModel.JobAssignments = viewModel.Jobs.Where(
                    x => x.JobID == jobID).Single().JobAssignments;
            }

            return View(viewModel);
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            var client = new Client();
            client.Jobs = new List<Job>();
            PopulateAssignedJobData(client);
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,FirstName,LastName,Email,ExtNo")]
        Client client, string[] selectedJobs)
        {
            {
                if (selectedJobs != null)
                {
                    client.Jobs = new List<Job>();
                    foreach (var job in selectedJobs)
                    {
                        var jobToAdd = db.Jobs.Find(int.Parse(job));
                        client.Jobs.Add(jobToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                PopulateAssignedJobData(client);
                return View(client);
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients
            .Include(i => i.ClientOffice)
            .Include(i => i.Jobs)
            .Where(i => i.ClientID == id)
            .Single();
            PopulateAssignedJobData(client);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        private void PopulateAssignedJobData(Client client)
        {
            var allJobs = db.Jobs;
            var clientJobs = new HashSet<int>(client.Jobs.Select(c => c.JobID));
            var viewModel = new List<AssignedJobData>();
            foreach (var job in allJobs)
            {
                viewModel.Add(new AssignedJobData
                {
                    JobID = job.JobID,
                    Title = job.Title,
                    Assigned = clientJobs.Contains(job.JobID)
                });
            }
            ViewBag.Jobs = viewModel;
        }

        // POST: Client/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] selectedJobs)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientToUpdate = db.Clients
               .Include(i => i.ClientOffice)
               .Include(i => i.Jobs)
               .Where(i => i.ClientID == id)
               .Single();

            if (TryUpdateModel(clientToUpdate, "",
               new string[] { "LastName", "FirstMidName", "Email", "ExtNo", "ClientOffice" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(clientToUpdate.ClientOffice.Location))
                    {
                        clientToUpdate.ClientOffice = null;
                    }
                    UpdateClientJobs(selectedJobs, clientToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedJobData(clientToUpdate);
            return View(clientToUpdate);
        }
        private void UpdateClientJobs(string[] selectedJobs, Client clientToUpdate)
        {
            if (selectedJobs == null)
            {
                clientToUpdate.Jobs = new List<Job>();
                return;
            }

            var selectedJobsHS = new HashSet<string>(selectedJobs);
            var clientJobs = new HashSet<int>
                (clientToUpdate.Jobs.Select(c => c.JobID));
            foreach (var job in db.Jobs)
            {
                if (selectedJobsHS.Contains(job.JobID.ToString()))
                {
                    if (!clientJobs.Contains(job.JobID))
                    {
                        clientToUpdate.Jobs.Add(job);
                    }
                }
                else
                {
                    if (clientJobs.Contains(job.JobID))
                    {
                        clientToUpdate.Jobs.Remove(job);
                    }
                }
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients
              .Include(i => i.ClientOffice)
              .Where(i => i.ClientID == id)
              .Single();

            db.Clients.Remove(client);

            var department = db.Departments
                .Where(d => d.ClientID == id)
                .SingleOrDefault();
            if (department != null)
            {
                department.ClientID = null;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}