using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class PagingModel
    {
        public int CurrentPage { get; set; }
        public int TotalItem { get; set; }

        public int LimitItem { get; set; }

        public PagingModel()
        {
            LimitItem = 20;
        }
        public PagingModel(int currentpage = 1, int totalitem = 1, int limititem = 20)
        {
            this.CurrentPage = currentpage;
            this.LimitItem = limititem;
            this.TotalItem = totalitem;
        }
    }
}
