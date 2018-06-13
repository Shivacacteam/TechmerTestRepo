using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace TechmerVision
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A bundle configuration. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Registers the bundles described by bundles. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="bundles">  The bundles. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-animate.js",
                //"~/Scripts/ui-bootstrap-tpls-1.2.5.min.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-pageslide-directive.js",
                "~/Scripts/angular-touch*",
                "~/Scripts/AngularFileUpload/ng-file-upload.js",
                "~/Scripts/AngularFileUpload/ng-file-upload-shim.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls*",
                "~/Scripts/interact.js",
                "~/Scripts/rzslider.min.js"
                //"~/Scripts/angular-messages.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/_run.js",
                "~/Scripts/app/app.js",
                "~/Scripts/app/uploader.js",
                "~/Scripts/app/colorSelection.js",
                "~/Scripts/app/gradientGrid.js",
                "~/Scripts/app/pixelator.js",
                "~/Scripts/app/recolor.js",
                "~/Scripts/app/es6-promise.js",
                "~/Scripts/app/html2canvas.js",
                "~/Scripts/app/html2canvas.svg.js",
                "~/Scripts/app/gridPalette.js",
                "~/Scripts/app/gridCompare.js",
                "~/Scripts/app/favoriteColors.js",
                "~/Scripts/app/productLibrary.js",
                "~/Scripts/app/sampleRequest.js",
                "~/Scripts/app/productList.js",
                 "~/Scripts/app/sampleRequestGrid.js",
                "~/Scripts/app/regionCapture.js",
                "~/Scripts/app/assetPalette.js",
                "~/Scripts/app/notifyReg.js",
                "~/Scripts/app/colorProvider.js",
                "~/Scripts/gapi.js",
                "~/Scripts/fb.js",
                "~/Scripts/app/inspirationProviders.js",
                "~/Directives/gallerySelector/gallerySelector.js",
                "~/Scripts/app/colorAnalyzer.js",
                "~/Scripts/app/filters.js",
                "~/bower_components/angular-masonry/angular-masonry.js",
                "~/Scripts/chroma.min.js",

                "~/Scripts/app/services.js"
                ));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css",
                 "~/Content/techmer.css",
                 "~/Content/techmer-table.css",
                 "~/Content/rzslider.min.css"));
        }
    }
}
