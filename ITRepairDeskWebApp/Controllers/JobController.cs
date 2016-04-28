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
using PagedList;
using System.Data.Entity.Infrastructure;

using ITRepairDeskWebApp.Controllers;
using ITRepairDeskWebApp.ViewModels;

namespace ITRepairDeskWebApp.Controllers
{
    public class JobController : Controller
    {
        private ITRepairDeskWebAppContext db = new ITRepairDeskWebAppContext();

        // GET: Job
        //public ActionResult Index()
        //{
        //    var jobs = db.Jobs.Include(j => j.Department);
        //    return View(jobs.ToList());

        //}

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Status" ? "Priority" : "Status";

             if (searchString != null)
   {
      page = 1;
   }
   else
   {
      searchString = currentFilter;
   }

   ViewBag.CurrentFilter = searchString;

            var jobs = from s in db.Jobs
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(s => s.Title.Contains(searchString)
                                       || s.Detail.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    jobs = jobs.OrderByDescending(s => s.Title);
                    break;
                case "Status":
                    jobs = jobs.OrderBy(s => s.Status);
                    break;
                case "Priority":
                    jobs = jobs.OrderByDescending(s => s.Priority);
                    break;
                default:
                    jobs = jobs.OrderBy(s => s.Title);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(jobs.ToPagedList(pageNumber, pageSize));
        }


        // GET: Job/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Job/Create
        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,Title,Detail,Status,Priority,DepartmentID")]Job job)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Jobs.Add(job);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(job.DepartmentID);
            return View(job);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            PopulateDepartmentsDropDownList(job.DepartmentID);
            return View(job);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var jobToUpdate = db.Jobs.Find(id);
            if (TryUpdateModel(jobToUpdate, "",
               new string[] { "Title", "Detail", "Status", "Priority", "DepartmentID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDepartmentsDropDownList(jobToUpdate.DepartmentID);
            return View(jobToUpdate);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in db.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }



        // GET: Job/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
