using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Company
    {
        public string AccountId { get; set; }
        public string BusinessName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public int Zipcode { get; set; }
    }
}
