using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShop.Resource;
using System.IO;

namespace System.Web.Mvc
{
    public static class HTMLExtensions
    {
        public static string Image(this UrlHelper url, string imagePath, bool isContentRoot = false)
        {
            string folderPath = isContentRoot ? SiteResource.SRC_CONTENT_IMAGE_DIR : SiteResource.SRC_IMAGES_DIR;
            return url.Content(string.Format(folderPath, imagePath));
        }
        
    }
}