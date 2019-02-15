using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractorFind.Models
{
    public class Bid
    {
        public string Id { get; set; }
        public string Price { get; set; }
        public string CompanyId { get; set; }
        public string DateCreated { get; set; }
    }
}