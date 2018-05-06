using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        Entities db = null;
        public OrderDetailDao()
        {
            db = new Entities();
        }

    }
}
