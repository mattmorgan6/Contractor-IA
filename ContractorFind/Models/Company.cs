using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContractorFind.Models
{
    public class Company
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Business Name cannot be longer than 50 characters, and must be at least three characters long.")]
        public string BusinessName { get; set; }

        [StringLength(24, ErrorMessage = "Phone Number cannot be longer that 24 characters.")]
        public string PhoneNumber { get; set; }

        [StringLength(128, ErrorMessage = "Description cannot be longer that 128 characters.")]
        public string Description { get; set; }

        public int Zipcode { get; set; }
    }
}