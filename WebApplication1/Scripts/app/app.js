var angApp = angular.module('TechmerVision', ["ngFileUpload", 'uploader', 'pixelator', 'colorSelection', 'gradientGrid', 'reColor', 'ngAnimate', , 'ui.bootstrap', 'ngRoute', 'ngResource', 'ngTouch', 'gridPalette', 'pageslide-directive', 'rzModule', 'techmerServices', 'gridCompare', 'favoriteColors', 'Techmer.ProductLibrary', 'techmerRegionCapture', 'techmerAssetPalette', 'Techmer.colorAnalyzer', 'Techmer.Filters', 'Techmer.SampleList', 'productList', 'requestGrid']);

function Alert(elm,type, msg) {
    var div = "";
    switch (type) {
        case 1:
            div = "<div id='alert' class='alert alert-success pull-right alertmain' style='z-index: 1052;position: absolute;right: 0;top: 90%; '><a class='close' style='margin-top: -13px;margin-right: 0%;' data-dismiss='alert'>&times;</a><strong>Well done! </strong> " + msg + "</div>";
            break;
        case 2:
            div = "<div id='alert' class='alert alert-danger pull-right alertmain' style='z-index: 1052;position: absolute;right: 0;top: 90%;'><a class='close' style='margin-top: -13px; margin-right: 0%;' data-dismiss='alert'>&times;</a><strong>Warning! </strong> " + msg + "</div>";
            break;
    }
    jQuery(elm).append(div);
    setTimeout(function () {
        jQuery('#alert').remove();
    }, 3000);

}


angApp.run(['$q', '$rootScope', '$location', '$window', function ($q, $rootScope, $location, $window) {
    $rootScope
       .$on('$routeChangeSuccess',
           function (event) {

               if (!$window.ga)

                   return;

               $window.ga('send', 'pageview', { page: $location.path() });
           })
}]);


angApp.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when('/', {
            //templateUrl: '/pages/inspiration.html',
            templateUrl: '/pages/inspiration',
            controller: 'TechmerVisionController'
        })
        .when('/visualization', {
            templateUrl: '/pages/visualization.html',
            controller: 'TechmerVisionController'
        })
        .when('/compare', {
            templateUrl: '/pages/compare.html',
            controller: 'TechmerVisionController'
        })
        .when('/request', {
            templateUrl: '/pages/request.html',
            controller: 'TechmerVisionController'
        })
        .when('/request_Details', {
            templateUrl: '/partials/SampleRequest.html',
            controller: 'TechmerVisionController'
        })
}])



angApp.controller('TechmerVisionController', ["$scope", "$routeParams", "$window", "$q", "$rootScope", "$log", function ($scope, $routeParams, $window, $q, $rootScope, $log) {

    var loadingCount = 0;
    $("#processer").fadeOut();
    //$rootScope.process = false;
    //debugger;
    return {
        request: function (config) {
            $rootScope.process = true;
            return config;
        },
        requestError: function (rejection) {
            $rootScope.process = false;
            //$log.error('Request error:', rejection);
            return $q.reject(rejection);
        },
        response: function (response) {
            $rootScope.process = false;
            return response;
        },
        responseError: function (rejection) {
            $rootScope.process = false;

            return $q.reject(rejection);
        },


    };


    /*
    $window.fbAsyncInit = function () {
        FB.init({
            appId: '1721939471382143',
            status: true,
            cookie: true,
            xfbml: true,
            version: 'v2.4'
        });
    }
    
    (function(d, s, id){
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {return;}
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));
      */

    $scope.$on('clearWorkspace', function (event, args) {
        ProductPalette.clearProductPalette(args.workspace);
    })

}])