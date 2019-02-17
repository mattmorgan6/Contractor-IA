using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int CompanyId { get; set; }
        public string DateCreated { get; set; }
        public int GigId { get; set; }
    }
}
