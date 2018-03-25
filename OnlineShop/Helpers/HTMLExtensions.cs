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
        #region Render View
        public static string RenderViewToString(Controller controller, string viewName, string masterViewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, masterViewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }

        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.ToString();
            }
        }
        #endregion
        public static string Image(this UrlHelper url, string imagePath, bool isContentRoot = false)
        {
            string folderPath = isContentRoot ? SiteResource.SRC_CONTENT_IMAGE_DIR : SiteResource.SRC_IMAGES_DIR;
            return url.Content(string.Format(folderPath, imagePath));
        }
        
    }
}