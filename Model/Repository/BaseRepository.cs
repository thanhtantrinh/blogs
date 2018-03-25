using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Common;

namespace Model.Repository
{
    public class BaseRepository
    {
        public static string conn = "";
        
        public BaseRepository(string sqlConnString="")
        {
            conn = sqlConnString;
        }
    }
}
