using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContractorFind.Models
{
    public class Gig
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 6, ErrorMessage = "Title must be at least 6 characters, and cannot be longer than 50 characters")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "Type cannot be longer that 50 characters.")]
        public string Type { get; set; }

        public int Footprint { get; set; }    //in squarefeet

        [StringLength(3000, MinimumLength = 6, ErrorMessage = "Description cannot be longer that 3000 characters.")]
        public string Description { get; set; }

        [StringLength(10, MinimumLength = 5, ErrorMessage = "You need to enter a valid ZipCode")]      //sets up range of data allowed to be entered
        public string Zipcode { get; set; }

        public int Price { get; set; }

        [DisplayName("Date Created")]
        public string CreationDate { get; set; }
        [DisplayName("Price")]
        public string PriceMessage { get; set; }


        public void PriceToString()
        {
            if (Price == -2 || Price == 0)
            {
                PriceMessage = "No bids";
            }
            else
            {
                PriceMessage = "$" + Price.ToString();
            }
        }

    }
}