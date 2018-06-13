var app = angular.module("Techmer.SampleList", ["techmerServices"]);
var Products = [];
var BaseColors = [];
var RequestGrids = [];
app.directive("sampleList", [function () {
    return {
        scope: {},
        restrict: 'E',
        templateUrl: "/partials/SampleList.html",

        controller: ["$rootScope", "$scope", "$element", "$attrs", "$uibModal", "$location", "ProductPalette", "Workspaces", "SharingPalette", "PersonalPalette", "SamplePalette", function ($rootScope, $scope, $element, $attrs, $modal, $location, ProductPalette, Workspaces, SharingPalette, PersonalPalette, SamplePalette) {

            $scope.isOpen = true;
            $scope.SelectedProduct = [];

            $rootScope.SampleRequest = $scope;
            function loadProducts() {
                $("#processer").fadeIn(500);
                SamplePalette.list().then(
                   function (products) {
                       $scope.SampleProducts = products;
                       $("#processer").fadeOut(1000);
                   }
               )


            }

            //Add Model
            $scope.requestDetails = function (product) {
                if ($scope.opening) {
                    return;
                }

                $rootScope.SampleRequest.SelectedProduct = product;
                $scope.opening = true;
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: "/partials/SampleRequest.html",
                    controller: "requestDetailsCtrl",
                    size: "xl"
                });

                modalInstance.result.then(function (product) {
                    $scope.opening = false;
                }, function () {
                    $scope.opening = false;
                    //$log.info('Modal dismissed at: ' + new Date());
                });
            };
            $scope.opening = false;
            loadProducts();
        }]
    }
}]);


app.controller("requestDetailsCtrl", ["$rootScope", "$scope", "$window", "$uibModal", "$uibModalInstance", "$timeout", "ProductTemplate", "SharingTemplate", "SamplePalette", "Workspaces", function ($rootScope, $scope, $window, $modal, $modalInstance, $timeout, ProductTemplate, SharingTemplate, SamplePalette, Workspaces) {
    $scope.isOpen = true;
    $scope.itemsPerPage = 12;
    $scope.currentPage = 1;
    $scope.selected = {};
    $scope.currentProduct = [];
    $scope.Products = Products;
    $scope.BaseColors = BaseColors;
    $scope.SelectedProduct = [];
    $scope.SampleRequestData = [];
    if ($rootScope.SampleRequest.SelectedProduct) {
        $scope.SampleRequestData = $rootScope.SampleRequest.SelectedProduct;
        $scope.SelectedProduct = $rootScope.SampleRequest.SelectedProduct.publicRequestAssetlist;
    }

    $rootScope.SampleRequest = $scope;

    loadWorkspace();
    function loadWorkspace() {
        Workspaces.currentWorkspace().then(function (workspaces) {
            $scope.currentWorkspace = workspaces;
        });
    }

    if ($scope.SampleRequestData.status == true) {
        // btnAddRequest
        $scope.button = {};
        $scope.button.disabled = true;
    }

    function loadProducts() {
        $("#processer").fadeIn(500);
        SamplePalette.list().then(
           function (products) {
               $scope.SampleProducts = products;
               setTimeout(function () {
                   $window.location.reload();
               }, 500);
               $("#processer").fadeOut(1000);
           }
       )

    }

    $scope.addRequest = function () {
        $("#processer").fadeIn(500);
        if ($scope.SampleRequestData.id > 0) {
            SamplePalette.updateRequest($scope.SampleRequestData.id, $scope.SampleRequestData, $scope.SelectedProduct).then(function (result) {
                $modalInstance.close($scope.selected.productTemplate);
                $("#processer").fadeOut(1000);
                loadProducts();
            });

        }
        else {
            SamplePalette.addRequest($scope.currentWorkspace.userId, $scope.SampleRequestData.projectName, $scope.SampleRequestData.notes, $scope.SelectedProduct).then(function (result) {
                //$rootScope.$broadcast('ProductLibraryChanged');
                $modalInstance.close($scope.selected.productTemplate);
                loadProducts();
            });
        }
    }

    $scope.updateRequest = function () {
        SamplePalette.updateRequest($scope.SampleRequestData, $scope.SelectedProduct).then(function (result) {
            $rootScope.$broadcast('ProductLibraryChanged');
        });
    }


    $scope.deleteSampleRequest = function () {

        $("#processer").fadeIn(500);
        if ($scope.SampleRequestData.length == 0) {
            $modalInstance.close($scope.selected.productTemplate);
        }
        else {
            SamplePalette.deleteProduct($scope.SampleRequestData)
            $modalInstance.close($scope.selected.productTemplate);
        }
        loadProducts();

        $("#processer").fadeOut(1000);
        //$rootScope.$broadcast('ProductLibraryChanged');
    }

    $scope.deleteSampleRequestAsset = function (product) {

        $scope.copydata = $scope.SelectedProduct;
        $scope.SelectedProduct = [];
        angular.forEach($scope.copydata, function (value) {
            if (value.assetId != product.assetId)
                $scope.SelectedProduct.push(value);
        });
    }

    $scope.ok = function () {
        $modalInstance.close($scope.selected.productTemplate);
    };


    $scope.$on('ProductLibraryChanged', function (event, args) {
        loadProducts();
    });

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

}]);

