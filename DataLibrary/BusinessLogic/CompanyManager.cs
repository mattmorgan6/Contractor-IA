using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    class CompanyManager
    {

        public static int PutInGig(string accountId, string businessName, int phoneNumber, string description)
        {
            Company data = new Company
            {
                AccountId = accountId,
                BusinessName = businessName,
                PhoneNumber = phoneNumber,
                Description = description
            };

            string sql = @"INSERT INTO dbo.Gigs (OwnerId, Title, Type, Footprint, Description, Zipcode, Price, CreationDate) 
                            VALUES (@OwnerId, @Title, @Type, @Footprint, @Description, @Zipcode, @Price, @CreationDate);";

            return SqlDataAccess.SaveData(sql, data);     //goes to the Sql DataAccess class
        }

    }
}
