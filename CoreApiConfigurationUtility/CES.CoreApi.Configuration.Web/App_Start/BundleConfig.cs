using System.Web.Optimization;

namespace CES.CoreApi.Configuration.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new StyleBundle("~/bundles/mini-spa/style").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/mini-spa/script").Include(
                      "~/Scripts/angular.min.js",
                      "~/Scripts/angular-route.min.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                      "~/app/ServiceConfigApp.js",
                      "~/app/DataService.js",
                      "~/app/forms/listController.js",
                      "~/app/forms/permissionsController.js",
                      "~/app/forms/settingsController.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
