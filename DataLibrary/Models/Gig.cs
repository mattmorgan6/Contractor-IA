using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLibrary.Models
{
    public class Gig
    {
        public string OwnerId { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }
        public int Footprint { get; set; }    //in squarefeet
        public string Description { get; set; }
        public int Zipcode { get; set; }
        public int Price { get; set; }  //probably gonna be null

        public string CreationDate { get; set; }
    }
}