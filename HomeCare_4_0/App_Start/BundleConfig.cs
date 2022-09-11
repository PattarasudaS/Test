using System.Web;
using System.Web.Optimization;

namespace HomeCare_4_0
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(

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

            /// Old css
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",                                                
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(                      
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/bootstrap-responsive.min.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/style.css",
                      "~/Content/css/pages/dashboard.css"));

            // Hnoy Add Drop Down Chosen At 16.03.2017
            bundles.Add(new ScriptBundle("~/bundles/jquery_chosen").Include(
                        "~/Scripts/chosen/chosen.jquery.js",
                        "~/Scripts/chosen/docsupport/prism.js"));
            // Hnoy Add Drop Down Chosen At 16.03.2017
            bundles.Add(new StyleBundle("~/bundles/DropDown_chosen").Include(
                        "~/Content/css/chosen.css"));
        }
    }
}
