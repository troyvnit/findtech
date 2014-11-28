using System.Web;
using System.Web.Optimization;

namespace FindTech.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/progressive/css").Include(
                      "~/css/buttons/buttons.css",
                    "~/css/buttons/social-icons.css",
                    "~/css/font-awesome.min.css",
                    "~/css/bootstrap.min.css",
                    "~/css/custom.bootstrap.css",
                    "~/css/jslider.css",
                    "~/css/settings.css",
                    "~/css/jquery.fancybox.css",
                    "~/css/animate.css",
                    "~/css/video-js.min.css",
                    "~/css/morris.css",
                    "~/css/royalslider/royalslider.css",
                    "~/css/royalslider/skins/minimal-white/rs-minimal-white.css",
                    "~/css/layerslider/layerslider.css",
                    "~/css/ladda.min.css",
                    "~/css/datepicker.css",
                    "~/css/jquery.scrollbar.css",
                    "~/css/style.css",
                    "~/css/responsive.css",
                    "~/css/customizer/pages.css",
                    "~/css/customizer/home-pages-customizer.css",
                    "~/css/bootstrap-tagsinput.css",
                    "~/css/ie/ie.css"));

            bundles.Add(new ScriptBundle("~/progressive/js").Include(
                      "~/js/bootstrap.min.js",
                    "~/js/price-regulator/jshashtable-2.1_src.js",
                    "~/js/price-regulator/jquery.numberformatter-1.2.3.js",
                    "~/js/price-regulator/tmpl.js",
                    "~/js/price-regulator/jquery.dependClass-0.1.js",
                    "~/js/price-regulator/draggable-0.1.js",
                    "~/js/price-regulator/jquery.slider.js",
                    "~/js/jquery.carouFredSel-6.2.1-packed.js",
                    "~/js/jquery.touchSwipe.min.js",
                    "~/js/jquery.elevateZoom-3.0.8.min.js",
                    "~/js/jquery.imagesloaded.min.js",
                    "~/js/jquery.appear.js",
                    "~/js/jquery.sparkline.min.js",
                    "~/js/jquery.easypiechart.min.js",
                    "~/js/jquery.easing.1.3.js",
                    "~/js/jquery.fancybox.pack.js",
                    "~/js/isotope.pkgd.min.js",
                    "~/js/jquery.knob.js",
                    "~/js/jquery.stellar.min.js",
                    "~/js/jquery.selectBox.min.js",
                    "~/js/jquery.royalslider.min.js",
                    "~/js/jquery.tubular.1.0.js",
                    "~/js/SmoothScroll.js",
                    "~/js/country.js",
                    "~/js/spin.min.js",
                    "~/js/ladda.min.js",
                    "~/js/masonry.pkgd.min.js",
                    "~/js/morris.min.js",
                    "~/js/raphael.min.js",
                    "~/js/video.js",
                    "~/js/pixastic.custom.js",
                    "~/js/livicons-1.3.min.js",
                    "~/js/layerslider/greensock.js",
                    "~/js/layerslider/layerslider.transitions.js",
                    "~/js/layerslider/layerslider.kreaturamedia.jquery.js",
                    "~/js/revolution/jquery.themepunch.plugins.min.js",
                    "~/js/revolution/jquery.themepunch.revolution.min.js",
                    "~/js/bootstrapValidator.min.js",
                    "~/js/bootstrap-datepicker.js",
                    "~/js/jplayer/jquery.jplayer.min.js",
                    "~/js/jplayer/jplayer.playlist.min.js",
                    "~/js/jquery.scrollbar.min.js",
                    "~/js/holder.js",
                    "~/js/bootstrap-tagsinput.js",
                    "~/js/typeahead.js",
                    "~/js/main.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
