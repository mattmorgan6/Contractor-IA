using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        //dapper documentation: https://dapper-tutorial.net/dapper
        //dapper tester: https://dotnetfiddle.net/vIvUNm

        public static string GetConnectionString(string connectionName = "DefaultConnection")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;     //goes out and gets the connection string for the database.
        }

        public static List<T> LoadSingularData<T>(string sql, string id)   //currrent is the Id
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))     //grabs the connection string from the method above.
            {
                return cnn.Query<T>(sql, new { GigId = id }).ToList();      //returns a list of generics from the database
            }
        }

        public static List<T> LoadData<T>(string sql, string current)   //currrent is the Id
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))     //grabs the connection string from the method above.
            {
                return cnn.Query<T>(sql, new { OwnerId = current }).ToList();      //returns a list of generics from the database
            }
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))     //grabs the connection string from the method above.
            {
                return cnn.Query<T>(sql).ToList();      //returns a list of generics from the database
            }
        }

        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))     //grabs the connection string from the method above.
            {
                return cnn.Execute(sql, data);      //puts in the data to save in db. Returns the number of records affected.
            }
        }


    }
}
