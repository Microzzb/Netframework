using System.Web;
using System.Web.Optimization;

namespace Netframework
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new Bundle("~/bundles/login").Include(
                              "~/Scripts/login.js"));
            bundles.Add(new Bundle("~/bundles/music").Include(
                              "~/Scripts/music.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/Index").Include(
                      "~/Content/index.css"));
            bundles.Add(new StyleBundle("~/Content/header-footer").Include(
                     "~/Content/header.css"));
            bundles.Add(new StyleBundle("~/Content/login").Include(
                     "~/Content/login.css"));
            bundles.Add(new StyleBundle("~/Content/User").Include(
                     "~/Content/user.css"));
            bundles.Add(new StyleBundle("~/Content/Music").Include(
         "~/Content/music.css"));
            bundles.Add(new StyleBundle("~/Content/MusicCreat").Include(
         "~/Content/MusicCreat.css"));
            bundles.Add(new StyleBundle("~/Content/EditData").Include(
         "~/Content/EditData.css"));
        }
    }
}
