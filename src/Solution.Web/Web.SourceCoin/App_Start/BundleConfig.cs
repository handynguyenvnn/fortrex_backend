using System.Web.Optimization;

namespace Web.SourceCoin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
#if DEBUG
            bundles.Add(new ScriptBundle("~/bundles/chartsjs_plugins").Include(
                      "~/Scripts/jquery-build-data.js",
                      "~/Content/Chart/linebo/core.js",
                      "~/Content/Chart/linebo/responsive.js",
                      "~/Scripts/ServerTime/jobTrade.js"
           ));
            bundles.Add(new StyleBundle("~/main/css").Include(
                      "~/Content/bootstrap.min.css",
                       "~/assets/UI/vendor/animate/animate.min.css",
                      "~/assets/UI/vendor/waves/waves.min.css",
                      "~/assets/UI/vendor/toastr/toastr.min.css",
                      "~/assets/UI/vendor/owlcarousel/css/owl.carousel.min.css",
                      "~/assets/UI/css/style.css",
                      "~/assets/UI/css/responsive.css",
                      "~/Content/loading-css.css"
                      )
                );
            bundles.Add(new StyleBundle("~/chartstool/css").Include(
                       "~/Content/Chart/linebo/tools.css",
                      "~/Content/Chart/linebo/topTool.css",
                      "~/Content/Chart/linebo/dark-unica.css",
                      "~/Content/Chart/linebo/style-slider-volume.css"
                      )
                );
            // Code removed for clarity.
            BundleTable.EnableOptimizations = false;
#else
            bundles.Add(new ScriptBundle("~/bundles/chartsjs_plugins").Include(
                        "~/Scripts/jquery-build-data.js",
                        "~/Content/Chart/linebo/core.js",
                        "~/Content/Chart/linebo/responsive.js",
                        "~/Scripts/ServerTime/jobTrade.js"
             ));
            bundles.Add(new StyleBundle("~/main/css").Include(
                      "~/Content/bootstrap.min.css",
                       "~/assets/UI/vendor/animate/animate.min.css",
                      "~/assets/UI/vendor/waves/waves.min.css",
                      "~/assets/UI/vendor/toastr/toastr.min.css",
                      "~/assets/UI/vendor/owlcarousel/css/owl.carousel.min.css",
                      "~/assets/UI/css/style.css",
                      "~/assets/UI/css/responsive.css",
                      "~/Content/loading-css.css"
                      )
                );
            bundles.Add(new StyleBundle("~/chartstool/css").Include(
                       "~/Content/Chart/linebo/tools.css",
                      "~/Content/Chart/linebo/topTool.css",
                      "~/Content/Chart/linebo/dark-unica.css",
                       "~/Content/Chart/linebo/style-slider-volume.css"
                      )
                );
            // Code removed for clarity.
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}
