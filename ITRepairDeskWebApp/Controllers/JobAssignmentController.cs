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

namespace ITRepairDeskWebApp.Controllers
{
    public class JobAssignmentController : Controller
    {
        private ITRepairDeskWebAppContext db = new ITRepairDeskWebAppContext();

        // GET: JobAssignment
        public ActionResult Index()
        {
            var jobAssignments = db.JobAssignments.Include(j => j.Job).Include(j => j.Technician);
            return View(jobAssignments.ToList());
        }

        // GET: JobAssignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobAssignment jobAssignment = db.JobAssignments.Find(id);
            if (jobAssignment == null)
            {
                return HttpNotFound();
            }
            return View(jobAssignment);
        }

        // GET: JobAssignment/Create
        public ActionResult Create()
        {
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "JobID");
            ViewBag.TechnicianID = new SelectList(db.Technicians, "TechnicianID", "FullName");
            return View();
        }

        // POST: JobAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobAssignmentID,JobID,TechnicianID")] JobAssignment jobAssignment)
        {
            if (ModelState.IsValid)
            {
                db.JobAssignments.Add(jobAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "JobID", jobAssignment.JobID);
            ViewBag.TechnicianID = new SelectList(db.Technicians, "TechnicianID", "FullName", jobAssignment.TechnicianID);
            return View(jobAssignment);
        }

        // GET: JobAssignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobAssignment jobAssignment = db.JobAssignments.Find(id);
            if (jobAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "JobID", jobAssignment.JobID);
            ViewBag.TechnicianID = new SelectList(db.Technicians, "TechnicianID", "FullName", jobAssignment.TechnicianID);
            return View(jobAssignment);
        }

        // POST: JobAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobAssignmentID,JobID,TechnicianID")] JobAssignment jobAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "JobID", jobAssignment.JobID);
            ViewBag.TechnicianID = new SelectList(db.Technicians, "TechnicianID", "FullName", jobAssignment.TechnicianID);
            return View(jobAssignment);
        }

        // GET: JobAssignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobAssignment jobAssignment = db.JobAssignments.Find(id);
            if (jobAssignment == null)
            {
                return HttpNotFound();
            }
            return View(jobAssignment);
        }

        // POST: JobAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobAssignment jobAssignment = db.JobAssignments.Find(id);
            db.JobAssignments.Remove(jobAssignment);
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
