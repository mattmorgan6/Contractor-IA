using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ContractorFind.Models
{
    public class Gig
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public int Footprint { get; set; }    //in squarefeet
        public string Description { get; set; }
        public int Zipcode { get; set; }
        public int Price { get; set; }

        [DisplayName("Date Created")]
        public string CreationDate { get; set; }
        [DisplayName("Price")]
        public string PriceMessage { get; set; }
    }
}