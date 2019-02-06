using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractorFind.Models;

namespace ContractorFind.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult CreateGig()
        {
            ViewBag.Message = "Create a gig";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGig(Gig gig)
        {
            ViewBag.Message = "Creat a gig";

            if (ModelState.IsValid)
            {
                string t = gig.Title;

                return RedirectToAction("Index");
            }

            return View();
        }






        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}