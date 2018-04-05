using System.Web;
using System.Web.Optimization;

namespace Blogs
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/adminloginjs/scripts").Include(
               "~/Scripts/adminjs/libs/jquery.js",
               "~/Scripts/adminjs/libs/tether.js",
               "~/Scripts/adminjs/libs/bootstrap.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/cpaneladmin/scripts").Include(
               "~/Scripts/adminjs/libs/jquery.js",
               "~/Scripts/adminjs/libs/tether.js",
               "~/Scripts/adminjs/libs/bootstrap.js",
               "~/Scripts/adminjs/libs/pace.js",
               "~/Scripts/adminjs/libs/highlight.pack.js",
               "~/Scripts/adminjs/app.js"
               ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
            "~/Content/css/font-awesome.css",
            "~/Content/css/simple-line-icons.css",
            "~/Content/css/vs2015.css",
            "~/Content/css/style.css"
            ));
            BundleTable.EnableOptimizations = false;
        }

        
    }
}
