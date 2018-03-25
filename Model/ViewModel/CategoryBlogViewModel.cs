using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
namespace Model.ViewModel
{
    public class CategoryBlogViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Content> Products { get; set; }
    }
}
