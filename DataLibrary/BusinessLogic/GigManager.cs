using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class GigManager
    {

        public static int PutInGig(string title, string type, int footprint, string description, int zipcode, int price)
        {
            Gig gig = new Gig
            {
                Title = title,
                Type = type,
                Footprint = footprint,
                Description = description,
                Zipcode = zipcode,
                Price = price,
                CreationDate = DateTime.Now
            };

            string sql = "connect to db todo";

            //todo return sql save
            return 1;
        }


    }
}
