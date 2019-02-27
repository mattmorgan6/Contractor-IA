using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    public static class BidManager
    {
        public static int PutInBid(int price, int companyId, string dateCreated, int gigId)
        {
            Bid bid = new Bid
            {
                Price = price,
                CompanyId = companyId,
                DateCreated = dateCreated,
                GigId = gigId
            };
            
            string sql = @"INSERT INTO dbo.Bids (Price, CompanyId, DateCreated, GigId) VALUES (@Price, @CompanyId, @DateCreated, @GigId)";

            return SqlDataAccess.SaveData(sql, bid);
        }

        public static List<Bid> LoadBids(string gigId)
        {
            string sql = @"SELECT * FROM dbo.Bids WHERE GigId = @OwnerId";

            return SqlDataAccess.LoadData<Bid>(sql, gigId);
        }

        public static List<Bid> LoadBidsForCompany(int companyId)
        {
            string sql = @"SELECT * FROM dbo.bids WHERE CompanyId = @OwnerId";

            return SqlDataAccess.LoadData<Bid>(sql, companyId.ToString());
        }

        public static List<Bid> FindLowestBid(int id)
        {
            // string sql = @"SELECT * FROM dbo.bids WHERE GigId = @OwnerId";
            string sql = @"SELECT MIN(Price) AS Price FROM dbo.bids WHERE GigId = @Id";

            return SqlDataAccess.LoadNum<Bid>(sql, id);
        }
    }
}
