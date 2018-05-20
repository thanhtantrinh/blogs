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

        /// <summary>
        /// Get Image Url for the product
        /// </summary>
        /// <param name="url"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static string ImageProductLarge(this UrlHelper url, string imageName)
        {
            string imageUrl = String.Empty;
            if (!String.IsNullOrWhiteSpace(imageName))
            {
                imageUrl = url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_LARGE, imageName));
            }
            else
            {
                imageUrl = url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_LARGE, "coming-soon.jpg"));
            }
            return imageUrl;
            //return url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_LARGE, imageName));
        }

        public static string ImageProductMedium(this UrlHelper url, string imageName="")
        {
            string imageUrl = String.Empty;
            if (!String.IsNullOrWhiteSpace(imageName))
            {
                imageUrl = url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_MEDIUM, imageName));               
            }
            else
            {
                imageUrl = url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_MEDIUM, "coming-soon.jpg"));
            }
            return imageUrl;
        }
        public static string ImageProductMin(this UrlHelper url, string imageName)
        {

            string imageUrl = String.Empty;
            if (!String.IsNullOrWhiteSpace(imageName))
            {
                imageUrl = url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_MIN, imageName));
            }
            else
            {
                imageUrl = url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_MIN, "coming-soon.jpg"));
            }
            return imageUrl;

            //return url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_MIN, imageName));
        }
        public static string ImageProductSmall(this UrlHelper url, string imageName)
        {
            string imageUrl = String.Empty;
            if (!String.IsNullOrWhiteSpace(imageName))
            {
                imageUrl = url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_SMALL, imageName));
            }
            else
            {
                imageUrl = url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_SMALL, "coming-soon.jpg"));
            }
            return imageUrl;

            //return url.Content(string.Format(SiteResource.SRC_IMAGES_PRODUCT_SMALL, imageName));
        }
    }
}