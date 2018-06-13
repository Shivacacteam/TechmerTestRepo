var app = angular.module("productList", ["techmerServices"]);

app.directive("productList", [function () {
    return {
        scope: {},
        restrict: 'E',
        templateUrl: "/partials/productList.html",
        controller: ["$rootScope", "$scope", "$element", "$attrs", "$uibModal", "ProductPalette", "ProductTemplate", "Workspaces", "SharingPalette", "PersonalTemplate", function ($rootScope, $scope, $element, $attrs, $modal, ProductPalette, ProductTemplate, Workspaces, SharingPalette, PersonalTemplate) {
            $scope.isOpen = true;

            function loadProducts() {
                //ProductPalette.allProducts().then(
                //    function (products) {
                //        //$scope.Products = products;
                //    }
                // )
                $("#processer").fadeIn(500);
                ProductPalette.list().then(
                    function (products) {
                    $scope.IsShared = false;
                    $scope.productTemplates = products;
                    $scope.selected = {
                        productTemplate: products[0]
                    };
                    $("#processer").fadeOut(1000);
                });
            }

            $scope.selectProduct = function (product) {
                //$scope.SampleProductData = [{ assetId: 0, assetType: '', assetbackground: '', assetTitle: '' }];
                var IsNew = true;
                angular.forEach($rootScope.SampleRequest.SelectedProduct, function (value) {
                    if (value.assetId == product.id) {
                        IsNew = false;
                    }
                });

                if (IsNew) {
                    $scope.SampleProductData = [];
                    $scope.SampleProductData.assetId = product.id;
                    $scope.SampleProductData.assetType = 'Product';
                    $scope.SampleProductData.assetTitle = product.title;
                    $scope.SampleProductData.assetbackground = product.image;

                    $scope.IsShared = false;
                    $rootScope.SampleRequest.SelectedProduct.push($scope.SampleProductData);
                    //$rootScope.SampleRequest.Products.push($scope.SampleProductData);
                   // $scope.selected.productTemplate = product;
                }
                else {
                    Alert($("#SampleRequest"), 2, '! Seeding request already exists.');
                }
            }
            $scope.opening = false;
            loadProducts();
        }]
    }
}]);
