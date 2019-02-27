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

        public static int PutInGig(string ownerId, string title, string type, int footprint, string description, string zipcode, int price)
        {
            Gig data = new Gig      //creates a gig object specifically to put into the database
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

            return SqlDataAccess.SaveData(sql, data);     //goes to the SqlDataAccess class and runs the SaveData method to put data in the table
        }

        public static List<Gig> LoadGig(string id)
        {
            string sql = @"SELECT Id, Title, Type, Footprint, Description, ZipCode, Price, CreationDate FROM dbo.Gigs WHERE OwnerId = @OwnerId;";

            return SqlDataAccess.LoadData<Gig>(sql, id);
        }

        public static List<Gig> LoadGigs()
        {
            string sql = @"SELECT Id, Title, Type, Footprint, Description, ZipCode, Price, CreationDate FROM dbo.Gigs";

            return SqlDataAccess.LoadData<Gig>(sql);
        }

        public static Gig LoadSpecificGig(string gigId)
        {
            string sql = @"SELECT Id, Title, Type, Footprint, Description, Zipcode, Price, CreationDate FROM dbo.Gigs WHERE Id = @gigId";

            return (SqlDataAccess.LoadSingularData<Gig>(sql, gigId)).ElementAt(0);
        }


        public static int RemoveAGig(string gigId)
        {
            string sql = @"DELETE FROM dbo.Gigs WHERE Id = @gigId";

            return SqlDataAccess.DeleteData(sql, gigId);

        }
    }
}
