using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    public static class GigManager
    {

        public static int PutInGig(string ownerId, string title, string type, int footprint, string description, int zipcode, int price)
        {
            Gig data = new Gig
            {
                OwnerId = ownerId,
                Title = title,
                Type = type,
                Footprint = footprint,
                Description = description,
                Zipcode = zipcode,
                Price = price,
                CreationDate = DateTime.Now.ToString()
            };

            string sql = @"INSERT INTO dbo.Gigs (OwnerId, Title, Type, Footprint, Description, Zipcode, Price, CreationDate) 
                            VALUES (@OwnerId, @Title, @Type, @Footprint, @Description, @Zipcode, @Price, @CreationDate);";

            return SqlDataAccess.SaveData(sql, data);     //goes to the Sql DataAccess class
        }

        public static List<Gig> LoadGig(string id)
        {
            string sql = @"SELECT Title, Type, Footprint, Description, ZipCode, Price, CreationDate FROM dbo.Gigs WHERE OwnerId = @OwnerId;";

            return SqlDataAccess.LoadData<Gig>(sql, id);
        }
        

    }
}
