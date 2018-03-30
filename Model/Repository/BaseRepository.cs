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
        public static OnlineShopEntities entities;

        public BaseRepository(string sqlConnString="")
        {
            conn = sqlConnString;
            entities = new OnlineShopEntities();
        }

        public BaseRepository()
        {
            entities = new OnlineShopEntities();
            conn = entities.Database.Connection.ConnectionString;
        }

    }
}
