using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    public static class CompanyManager
    {

        public static int PutInCompany(string id, string businessName, string phoneNumber, string description, int zipcode)
        {
            Company data = new Company
            {
                AccountId = id,
                BusinessName = businessName,
                PhoneNumber = phoneNumber,
                Description = description,
                Zipcode = zipcode
            };

            string sql = @"INSERT INTO dbo.Companies (AccountId, BusinessName, PhoneNumber, Description, Zipcode) 
                            VALUES (@AccountId, @BusinessName, @PhoneNumber, @Description, @Zipcode);";

            return SqlDataAccess.SaveData(sql, data);     //goes to the Sql DataAccess class
        }

    }
}
