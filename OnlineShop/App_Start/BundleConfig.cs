using System.Web;
using System.Web.Optimization;

namespace OnlineShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                            "~/Scripts/JavaScript.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/smartmenus").Include(
                      "~/bower_components/smartmenus/dist/smartmenus.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                  "~/bower_components/bootstrap/dist/js/bootstrap.js"
                  ));

            bundles.Add(new StyleBundle("~/assets/ishop/css").Include(
                      "~/assets/ishop/css/Styles.css",
                    //"~/bower_components/smartmenus/src/css/sm-core-css.css",
                    "~/bower_components/smartmenus/src/addons/bootstrap/jquery.smartmenus.bootstrap.css"
                    //"~/bower_components/smartmenus/src/css/sm-simple/sm-simple.css"
                      ));

            bundles.Add(new StyleBundle("~/assets/vegetarian/css/styles.css").Include(
                "~/assets/vegetarian/css/styles.css"
          ));

            BundleTable.EnableOptimizations = false;
        }
    }
}
