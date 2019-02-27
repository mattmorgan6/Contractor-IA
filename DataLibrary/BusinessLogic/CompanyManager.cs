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

        public static int SetRole(string id, int roleId)
        {
            Role Role = new Role
            {
                UserId = id,
                RoleId = roleId
            };

            string sql = @"INSERT INTO dbo.AspNetUserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)";

            return SqlDataAccess.SaveData(sql, Role);
        }

        public static int RetrieveCompanyId(string userId)      //grabs the companyId for a gig.
        {
            string sql = @"SELECT Id FROM dbo.Companies WHERE AccountId = @gigId";

            return SqlDataAccess.LoadSingularData<int>(sql, userId).ElementAt(0);
        }

        public static Company LoadCompany(int companyId)
        {
            string sql = @"SELECT AccountId, BusinessName, PhoneNumber, Description, ZipCode FROM dbo.Companies WHERE Id = @GigId";

            return SqlDataAccess.LoadSingularData<Company>(sql, companyId.ToString()).ElementAt(0);
        }

    }
}
