var app = angular.module("Techmer.ProductLibrary", ["techmerServices"]);

app.directive("productLibrary", [function () {
    return {
        scope: {},
        restrict: 'E',
        templateUrl: "/partials/productLibrary.html",
        controller: ["$rootScope", "$scope", "$element", "$attrs", "$uibModal", "ProductPalette", "Workspaces", "SharingPalette", "PersonalPalette", function ($rootScope, $scope, $element, $attrs, $modal, ProductPalette, Workspaces, SharingPalette, PersonalPalette) {
            $scope.isOpen = true;
            function loadProducts() {
                ProductPalette.list().then(
                    function (products) {

                        $scope.Products = products;
                        $scope.productTemplates = products;
                        $scope.CurrentProduct = products[0];
                    }
                )

                SharingPalette.list().then(
                function (products) {
                    $scope.SharedProducts = products;

                })
                PersonalPalette.list().then(
                  function (products) {
                      $scope.ParsonalProducts = products;
                  })

            }

            $scope.deleteProductLibraryProduct = function (product) {
                ProductPalette.deleteProduct(product)
                loadProducts();
                $rootScope.$broadcast('ProductLibraryChanged');
            }

            $scope.selectProduct = function (product) {
                ProductPalette.selectProduct(product);
                $rootScope.$broadcast('ProductLibraryChanged');
            }

            //Add Model
            $scope.addProduct = function (event) {
                if ($scope.opening) {
                    return;
                }
                $scope.opening = true;
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: "/partials/productModal.html",
                    controller: "productTemplateCtrl",
                    size: "lg"
                });

                modalInstance.result.then(function (selectedProduct) {
                    $scope.opening = false;
                    Workspaces.list().then(function (workspaces) {
                      
                        ProductPalette.addProduct(selectedProduct, workspaces[0].id)
                        .then(function (result) {
                            $rootScope.$broadcast('ProductLibraryChanged');
                        });
                    })
                }, function () {
                    $scope.opening = false;
                    //$log.info('Modal dismissed at: ' + new Date());
                });
            };

            $scope.$on('ProductLibraryChanged', function (event, args) {
                loadProducts();
            });

            $scope.$on('clearWorkspace', function (event, args) {
                ProductPalette.clearProductPalette(args.workspace).then(function (result) {
                    $scope.Products = [result];
                    $scope.CurrentProduct = result;
                })
            });
            $scope.opening = false;
            loadProducts();
        }]
    }
}]);

app.controller("productTemplateCtrl", ["$rootScope", "$scope", "$uibModal", "$uibModalInstance", "ProductTemplate", "SharingTemplate", "PersonalTemplate", "Workspaces", "ProductPalette", function ($rootScope, $scope, $modal, $modalInstance, ProductTemplate, SharingTemplate, PersonalTemplate, Workspaces, ProductPalette) {

    $scope.itemsPerPage = 12;
    $scope.currentPage = 1;
    $scope.productTemplates = [];
    $scope.sharingTemplates = [];
    $scope.personalTemplates = [];
    $scope.selected = {};
    $scope.IsShared = false;
    $scope.IsUpload = false;
    loadProducts();
    function loadProducts() {
        $("#processer").fadeIn(500);
        SharingTemplate.query(function (data) {
            $scope.sharingTemplates = data;
        });


        PersonalTemplate.query(function (data) {
            $scope.personalTemplates = data;
        })

        ProductTemplate.query(function (data) {
            $scope.IsShared = false;
            $scope.productTemplates = data;
            $scope.selected = {
                productTemplate: data[0]
            };
            $("#processer").fadeOut(1000);
        });

    };

    $scope.selectProduct = function (product) {
        //$event.preventDefault(); 
        $scope.IsShared = false;
        $scope.selected.productTemplate = product;
    }

    //Share product active
    $scope.selectPersonalProduct = function (product) {
        $scope.IsShared = true;
        $scope.selected.productTemplate = product;
        //var sharedTemp = $scope.selected.productTemplate;
        $rootScope.sharedTemp = $scope.selected.productTemplate;
    }

    //Tab Active
    $scope.tabSelected = function (tab) {
     
        if (tab == "Personal") {
            $scope.IsShared = true;
            $scope.selected.productTemplate = '';
        }
       else if (tab == "Upload") {
           $scope.IsUpload = true;
           $scope.selected.productTemplate = '';
        }
        else {
            $scope.IsUpload = false;
            $scope.IsShared = false;
        }
    }

    $scope.ok = function () {
        $modalInstance.close($scope.selected.productTemplate);
    };

    //Share Model
    $scope.shareProduct = function (event) {
        if ($scope.opening) {
            return;
        }
        $scope.opening = true;
        var modalInstance = $modal.open({
            animation: true,
            templateUrl: "/partials/shareModal.html",
            controller: "shareTemplateCtrl",
            size: "lg"
        });

        modalInstance.result.then(function (selectedProduct) {
            $scope.opening = false;
            //Workspaces.list().then(function (workspaces) {
            //    ProductPalette.addProduct(selectedProduct, workspaces[0].id)
            //    .then(function (result) {
            //        $rootScope.$broadcast('ProductLibraryChanged');
            //    });
            //})
        }, function () {
            $scope.opening = false;
            //$log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.$on('ProductLibraryChanged', function (event, args) {
        loadProducts();
    });

    //Upload Template

    $scope.imageData = {};

    $scope.Selectedfile = function (file) {
      
        $scope.processFile(file);
        ProductPalette.ReadFile(file).then(function (imageData) {
            $scope.selected.productTemplate.image = imageData;
            $scope.selected.provider = 'Upload';
            $scope.selected.productTemplate = ProductPalette.getImage;
        });
    }



    $scope.processFile = function (file) {
        var fileReader = new FileReader();
        fileReader.onload = function (event) {
            var uri = event.target.result;
           // $scope.currentWorkspace.image = uri;
            //$scope.currentWorkspace.$save();
           // $scope.$apply();
           // $rootScope.$broadcast('InspirationChanged');
        };
       var result= fileReader.readAsDataURL(file);
    };


    $scope.UploadImage = function () {

        ProductPalette.UploadFile('title', $scope.selected.productTemplate.image, '1', "nitin.c@acapteam.com").then(function (result) {
            //$rootScope.$broadcast('ProductLibraryChanged');
        });
        
    }





    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

}]);

app.controller("shareTemplateCtrl", ["$rootScope", "$scope", "$uibModal", "$uibModalInstance", "ProductTemplate", "SharingPalette", "PersonalTemplate", "SharingTemplate", "ShareList", "Workspaces", function ($rootScope, $scope, $modal, $modalInstance, ProductTemplate, SharingPalette, PersonalTemplate, SharingTemplate, ShareList, Workspaces) {

    $scope.itemsPerPage = 12;
    $scope.currentPage = 1;
    $scope.personalTemplates = [];
    $scope.selected = {};
    $scope.IsShared = false;


    PersonalTemplate.query(function (data) {
        $scope.personalTemplates = data;
    })

    loadWorkspace();
    function loadWorkspace() {
        Workspaces.currentWorkspace().then(function (workspaces) {
            $scope.currentWorkspace = workspaces;
        });
    }

    $scope.selected.personalTemplates = $rootScope.sharedTemp;

    $scope.selected.currentlShareWith = [];
    $scope.ShareTemplate = { ShareType: '0' };

    function loadSharingData() {
        $("#processer").fadeIn(500);
        ShareList.query(function (result) {
            $scope.selected.currentlShareWith = result;
            var copydata = angular.copy($scope.selected.currentlShareWith);
            $scope.currentlShareWith = [];
            angular.forEach(copydata, function (value) {
                if (value.assetId == $rootScope.sharedTemp.id)
                    $scope.currentlShareWith.push(value);
            });
            $("#processer").fadeOut(500);
        });
    }

    function loadcurrentshare(assetid) {
        $("#processer").fadeIn(500);
        var copydata = angular.copy($scope.currentlShareWith);
        $scope.currentlShareWith = [];
        angular.forEach(copydata, function (value) {
            if (value.id != assetid)
                $scope.currentlShareWith.push(value);
        });
        $("#processer").fadeOut(500);

    }

    $scope.StopSharing = function (shared) {

        SharingPalette.deleteShared(shared);

        setTimeout(function () {
            loadcurrentshare(shared.id);
        }, 100);

    };


    $scope.ShareNow = function () {

        var Title = $("#SharedType option:selected").text();
      
        if ($scope.ShareTemplate.User != null) {

            if ($scope.currentWorkspace.userId == $scope.ShareTemplate.User) {
                Alert($("#SharingDetails"), 2, 'Failed ! Share the Product with other user only');
            }
            else {
                SharingPalette.shareTemplate($scope.ShareTemplate.User, Title, $scope.selected.personalTemplates.id).then(function (result) {
                    //$rootScope.$broadcast('ProductLibraryChanged');
                });
                $modalInstance.close($scope.selected.personalTemplates);
            }
        }
        else {
            Alert($("#SharingDetails"), 2, 'Failed ! Email Id must required for sharing a template');
        }

    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
    loadSharingData();
}]);