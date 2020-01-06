using System.Web;
using System.Web.Optimization;

namespace ContactManagement
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            #region Custom Layout page style and script section
            bundles.Add(new StyleBundle("~/Content/customLayoutCSS").Include(
                      "~//vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/vendors/font-awesome/css/font-awesome.min.css",
                      "~/Content/custom.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/customLayoutScriptUpper").Include(
                "~/vendors/jquery/dist/jquery.min.js",
                "~/vendors/fastclick/lib/fastclick.js",
                "~/vendors/nprogress/nprogress.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/customLayoutScriptLower").Include(
                "~/vendors/bootstrap/dist/js/bootstrap.min.js",
                "~/vendors/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js",
                "~/Scripts/custom.min.js"));
            #endregion
        }
    }
}
