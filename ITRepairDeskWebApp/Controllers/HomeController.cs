using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITRepairDeskWebApp.DAL;
using ITRepairDeskWebApp.ViewModels;


namespace ITRepairDeskWebApp.Controllers
{
    public class HomeController : Controller

    {
        private ITRepairDeskWebAppContext db = new ITRepairDeskWebAppContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<JobAssignmentDateGroup> data = from technician in db.Technicians
                                                      group technician by technician.JobAssignDate into dateGroup
                                                      select new JobAssignmentDateGroup()
                                                      {
                                                          JobAssignDate = dateGroup.Key,
                                                          TechnicianCount = dateGroup.Count()


        };
            return View(data.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}