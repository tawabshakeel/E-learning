using System.Web;
using System.Web.Optimization;

namespace WebApplication1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var jqueryCdnPath1 = "../../../ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js";
            
            bundles.Add(new ScriptBundle("~/bundles/jquery",jqueryCdnPath1).Include(
                "~/Scripts/modernizr-latest.js",
                "~/Scripts/custom.js",
                "~/Scripts/jquery.cslider.js",
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/da-slider.css",
                      "~/images/favicon.png",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/style.css"));
        }
    }
}
