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
            conn = entities.Database.Connection.ConnectionString;
        }

    }
}
