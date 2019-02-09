using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractorFind.Models;
using Microsoft.AspNet.Identity;
using DataLibrary.BusinessLogic;

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
            ViewBag.Message = "Create a gig";

            if (ModelState.IsValid)
            {
                string ownerId = User.Identity.GetUserId();

                int recordsCreated = GigManager.PutInGig(gig.Title, gig.Type, gig.Footprint, gig.Description, gig.Zipcode, -2); //-2 is the code for no price set

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