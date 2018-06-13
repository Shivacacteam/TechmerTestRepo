var app = angular.module('techmerInspirationProviders', ["ui.bootstrap", "techmerGallerySelector"]);

app.directive("inspirationProviderTechmer", function () {
    return {
        restrict: 'E',
        templateUrl: "/partials/inspirationProviders/Techmer.html",
        scope: {
            selected : "="
        },
        controller: ["$scope", "$q", "$http", "SampleInspirations", function ($scope, $q, $http, SampleInspirations) {
            
            $scope.sampleInspirations = [];

            SampleInspirations.listImages().then(function (data) {
                $scope.sampleInspirations = data;
            });
           
            $scope.selectInspiration = function (sampleInspiration) {
                $scope.selected.image = sampleInspiration.image;
                $scope.selected.provider = 'Techmer';
                $scope.selected.getImage = SampleInspirations.getImage;
            }
            
        }]
    };
});

app.directive("inspirationProviderUpload", function () {
    return {
        restrict: 'E',
        templateUrl: "/partials/inspirationProviders/Upload.html",
        scope: {
            selected: "="
        },
        controller: ["$scope", "$q", "$http", "InspirationUpload", function ($scope, $q, $http, InspirationUpload) {

            $scope.imageData = {};

            $scope.fileSelected = function (file) {
                InspirationUpload.ReadFile(file).then(function (imageData) {
                    $scope.selected.image = imageData;
                    $scope.selected.provider = 'Upload';
                    $scope.selected.getImage = InspirationUpload.getImage;
                });
            }
        }]
    };
});



