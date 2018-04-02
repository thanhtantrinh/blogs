using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class MenuItem
    {
        public long Id { get; set; }
        public long ParentId { get; set; }        
        public string Name { get; set; }
        public string Alias { get; set; }
        public int CatalogueId { get; set; }
        public string Status { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
    }
}
