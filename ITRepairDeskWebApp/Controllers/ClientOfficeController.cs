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
    public class ClientOfficeController : Controller
    {
        private ITRepairDeskWebAppContext db = new ITRepairDeskWebAppContext();

        // GET: ClientOffice
        public ActionResult Index()
        {
            var clientOffices = db.ClientOffices.Include(c => c.Client);
            return View(clientOffices.ToList());
        }

        // GET: ClientOffice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientOffice clientOffice = db.ClientOffices.Find(id);
            if (clientOffice == null)
            {
                return HttpNotFound();
            }
            return View(clientOffice);
        }

        // GET: ClientOffice/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "FullName");
            return View();
        }

        // POST: ClientOffice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,Location")] ClientOffice clientOffice)
        {
            if (ModelState.IsValid)
            {
                db.ClientOffices.Add(clientOffice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "FullName", clientOffice.ClientID);
            return View(clientOffice);
        }

        // GET: ClientOffice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientOffice clientOffice = db.ClientOffices.Find(id);
            if (clientOffice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "FullName", clientOffice.ClientID);
            return View(clientOffice);
        }

        // POST: ClientOffice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,Location")] ClientOffice clientOffice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientOffice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "FullName", clientOffice.ClientID);
            return View(clientOffice);
        }

        // GET: ClientOffice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientOffice clientOffice = db.ClientOffices.Find(id);
            if (clientOffice == null)
            {
                return HttpNotFound();
            }
            return View(clientOffice);
        }

        // POST: ClientOffice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientOffice clientOffice = db.ClientOffices.Find(id);
            db.ClientOffices.Remove(clientOffice);
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
