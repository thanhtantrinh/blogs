using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class SiteSetting
    {
        public string SiteName { get; set; }
        public string BaseURL { get; set; }

        public string ProductImagesMediumURL { get; set; }
        public string ProductImagesLargeURL { get; set; }
        public string ProductImagesSmallURL { get; set; }
        public string ProductImagesMinURL { get; set; }
        public SiteSetting()
        {
            this.SiteName = "";
            this.BaseURL = "";
            this.ProductImagesLargeURL = "~/Images/ImageStore/Products/large/";
            this.ProductImagesMediumURL = "~/Images/ImageStore/Products/medium/";            
            this.ProductImagesSmallURL = "~/Images/ImageStore/Products/small/";
            this.ProductImagesMinURL = "~/Images/ImageStore/Products/min/";

        }        
    }
}