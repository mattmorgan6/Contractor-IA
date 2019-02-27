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
        public ActionResult Index()     //the homepage for the application
        {
            return View();
        }

        public ActionResult About()     //the about page for the application
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]     //authorize means the user has to be signed in as a customer or a contractor.
        public ActionResult CreateGig()     //view where a customer can create a gig
        {
            ViewBag.Message = "Create a gig";

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGig(Gig gig)      //takes the data from view and adds the gig to the database.
        {
            ViewBag.Message = "Create a gig";

            if (ModelState.IsValid)
            {
                string ownerId = User.Identity.GetUserId();

                int recordsCreated = GigManager.PutInGig(ownerId, gig.Title, gig.Type, gig.Footprint, gig.Description, gig.Zipcode, -2);    //-2 is the code for no price set

                return RedirectToAction("CustomerCentral");
            }

            return View();
        }

        [Authorize]
        public ActionResult CustomerCentral()       //view where customer can view a list of their projects
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

                g.Price = FindLowestBid(Convert.ToInt32(g.Id));     //updates the price to the lowest bid.

                g.PriceToString();

                myListOfGigs.Add(g);
            }
            return View(myListOfGigs);
        }

        [Authorize]
        public ActionResult ViewBids(string gigId)      //View where contractor can see what bids he has made
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



        [Authorize(Roles = "Company")]      //this means the user must be signed in as a contractor
        public ActionResult CompanyCentral()        //View where contractors can see what projects are available to have bids on
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

                g.Price = FindLowestBid(Convert.ToInt32(g.Id));
                g.PriceToString();

                myListOfGigs.Add(g);
            }

            return View(myListOfGigs);
        }


        [Authorize(Roles = "Company")]
        public ActionResult PlaceBid(string id)     //View where contractor can place a bid on a specific gig
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

        [Authorize]
        public ActionResult SetNewBid()
        {
            return View();
        }


        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetNewBid(Bid bid, int Id)      //takes the data for the new bid and saves it
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

        [Authorize]
        public ActionResult ViewBidsMade()      //view for a contractor to see what bids have been made on a gig
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

        [Authorize]
        public ActionResult ViewCompany(int companyId)      //View for a customer to see what business made the bid on their project
        {
            var data = CompanyManager.LoadCompany(companyId);

            Company c = new Company
            {
                BusinessName = data.BusinessName,
                Description = data.Description,
                PhoneNumber = data.PhoneNumber,
                Zipcode = data.Zipcode
            };

            return View(c);
        }

        public ActionResult DeleteGig(string gigId)     //View confirms to the customer that he/she wants to delete the project
        {
            var data = GigManager.LoadSpecificGig(gigId);

            Gig gig = new Gig
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

            return View(gig);
        }

        public ActionResult DeleteGigPerm(string gigId)     //permanantly deletes the gig from the database
        {
            GigManager.RemoveAGig(gigId);
                
            return RedirectToAction("CustomerCentral");
        }


        public ActionResult Contact()       //View to show contact information. currently not visible on the nav bar
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string GetTheCurrentId()     //returns the id of the current user
        {
            string a = User.Identity.GetUserId();       //this works for getting the curent user id
            return a;
        }

        public int GetTheCurrentCompanyId()     //returns the id of the company that the contractor is linked to.
        {
            string userId = User.Identity.GetUserId();
            int companyId = CompanyManager.RetrieveCompanyId(userId);
            return companyId;
        }

        public ActionResult Error()     //returns error page for view
        {
            return View();
        }

        public int FindLowestBid(int id)        //finds the lowest bid on a specific gig from the database.
        {
            //find all bids with the gig id
            //find the lowest bid int

            var data = BidManager.FindLowestBid(id);
            return data.ElementAt(0).Price;
        }
    }
}