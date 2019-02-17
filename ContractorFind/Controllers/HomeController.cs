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

        [Authorize]
        public ActionResult CreateGig()
        {
            ViewBag.Message = "Create a gig";

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGig(Gig gig)
        {
            ViewBag.Message = "Create a gig";

            if (ModelState.IsValid)
            {
                string ownerId = User.Identity.GetUserId();

                int recordsCreated = GigManager.PutInGig(ownerId, gig.Title, gig.Type, gig.Footprint, gig.Description, gig.Zipcode, -2);    //-2 is the code for no price set

                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize]
        public ActionResult CustomerCentral()
        {
            ViewBag.Message = "View your projects.";

            string id = GetTheCurrentId();

            var data = GigManager.LoadGig(id);

            List<Gig> myListOfGigs = new List<Gig>();

            foreach (var row in data)
            {
                Gig g = new Gig
                {
                    Id = row.Id,
                    Title = row.Title,
                    Type = row.Type,
                    Footprint = row.Footprint,
                    Description = row.Description,
                    Zipcode = row.Zipcode,
                    Price = row.Price,
                    CreationDate = row.CreationDate
                };

                g.PriceToString();

                myListOfGigs.Add(g);

            }

            return View(myListOfGigs);
        }

        [Authorize]
        public ActionResult ViewBids(string gigId)
        {
            ViewBag.Message = "View the bids on this gig";

            var data = BidManager.LoadBids(gigId);

            List<Bid> listOfBids = new List<Bid>();

            foreach (var row in data)
            {
                Bid bid = new Bid()
                {
                    Id = row.Id,
                    Price = row.Price,
                    CompanyId = row.CompanyId,
                    DateCreated = row.DateCreated,
                    GigId = row.GigId
                };

                listOfBids.Add(bid);
            }

            return View(listOfBids);
        }



        [Authorize(Roles = "Company")]
        public ActionResult CompanyCentral()        //todo: sort by location, and add a bid page.
        {
            ViewBag.Message = "Bid on a project.";

            var data = GigManager.LoadGigs();

            List<Gig> myListOfGigs = new List<Gig>();

            foreach (var row in data)
            {
                Gig g = new Gig
                {
                    Id = row.Id,
                    Title = row.Title,
                    Type = row.Type,
                    Footprint = row.Footprint,
                    Description = row.Description,
                    Zipcode = row.Zipcode,
                    Price = row.Price,
                    CreationDate = row.CreationDate
                };

                g.PriceToString();

                myListOfGigs.Add(g);
            }

            return View(myListOfGigs);
        }


        public ActionResult PlaceBid(string id)
        {
            Gig gig;

            var data = GigManager.LoadSpecificGig(id);

            gig = new Gig
           {
               Id = data.Id,
               Title = data.Title,
               Type = data.Type,
               Footprint = data.Footprint,
               Description = data.Description,
               Zipcode = data.Zipcode,
               Price = data.Price,
               CreationDate = data.CreationDate
           };

            gig.PriceToString();

            return View(gig);
        }


        public ActionResult SetNewBid()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetNewBid(Bid bid, int Id)
        {
            if (ModelState.IsValid)
            {
                bid.CompanyId = GetTheCurrentCompanyId();

                bid.DateCreated = DateTime.Now.ToString();

                bid.GigId = Id;

                int recordsCreated  = BidManager.PutInBid(bid.Price, bid.CompanyId, bid.DateCreated, bid.GigId);

                return RedirectToAction("CompanyCentral");
            }

            return View();
        }

        public ActionResult ViewBidsMade()
        {
            int companyId = GetTheCurrentCompanyId();

            List<Bid> bidsList = new List<Bid>();              //go to the database and get all of the bids made by a company

            var data = BidManager.LoadBidsForCompany(companyId);

            foreach(var row in data)
            {
                bidsList.Add( new Bid  {
                Id = row.Id,
                Price = row.Price,
                CompanyId = row.CompanyId,
                DateCreated = row.DateCreated,
                GigId = row.GigId
                });
            }


            return View(bidsList);
        }




        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string GetTheCurrentId()
        {
            string a = User.Identity.GetUserId();       //this worked for getting the curent user id
            return a;
        }

        public int GetTheCurrentCompanyId()
        {
            string userId = User.Identity.GetUserId();
            int companyId = CompanyManager.RetrieveCompanyId(userId);
            return companyId;
        }
    }
}