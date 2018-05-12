using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Common;
using Model.EF;

namespace Model.Repository
{
    public class BaseRepository
    {
        public static string conn = "";
        public static Entities entities;

        public BaseRepository(string sqlConnString="")
        {
            conn = sqlConnString;
            entities = new Entities();
        }

        public BaseRepository()
        {
            entities = new Entities();
            var dbuser = ConfigurationManager.AppSettings["dbuser"] ?? "sa";
            var dbpass = ConfigurationManager.AppSettings["dbpass"] ?? "123456789";
            var dbserver = ConfigurationManager.AppSettings["dbserver"] ?? "TANTRINH\\SQLEXPRESS";
            var dbcatalog = ConfigurationManager.AppSettings["dbcatalog"] ?? ""; // name database
            var conn = "data source={0};initial catalog={1};user id={2};password={3}; Pooling=True";
            conn = String.Format(conn, dbserver, dbcatalog, dbuser, dbpass);
        }

    }
}
