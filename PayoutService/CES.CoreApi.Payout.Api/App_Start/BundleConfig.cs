using System.Web;
using System.Web.Optimization;

namespace CES.CoreApi.Payout.Api
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/MoneyTransfer/Payout/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/MoneyTransfer/Payout/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/MoneyTransfer/Payout/Scripts/bootstrap.js",
                      "~/MoneyTransfer/Payout/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/MoneyTransfer/Payout/Content/bootstrap.css",
                      "~/MoneyTransfer/Payout/Content/site.css"));
        }
    }
}
