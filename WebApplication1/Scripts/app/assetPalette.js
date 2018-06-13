(function () {
    var app = angular.module('techmerAssetPalette', ['techmerServices']);

    app.directive("assetPalette", ['Grids', 'ProductPalette', 'Workspaces',"AssetLayerSystem", function (Grids, ProductPalette, Workspaces,AssetLayerSystem) {
        return {
            restrict: 'E',
            templateUrl: "/partials/assetPalette.html",
            controller: ["$scope","$element","$attrs","$rootScope", function ($scope, $element, $attrs, $rootScope) {
               

                function loadGrids() {
                    Grids.list().then(
                        function (grids) {
                            $scope.Grids = grids;
                            $scope.CurrentGrid = grids[0];
                        }
                    )
                }

                function loadProducts() {
                    ProductPalette.list().then(
                        function (products) {
                            $scope.Products = products;
                            $scope.CurrentProduct = products[0];
                        }
                    )
                }

                function loadWorkspaces(){
                    Workspaces.list().then(
                        function (workspaces) {
                            $scope.Workspaces = workspaces
                            $scope.CurrentWorkspace = workspaces[0];
                        }
                    )
                }

                function loadData() {
                    loadWorkspaces();
                    loadGrids();
                    loadProducts();
                }

                function initAssetPalette() {
                    $scope.showAssets = false;
                    if ($attrs.psSide) {
                        $scope.slideDirection = $attrs.psSide;
                    }
                    else {
                        $scope.slideDirection = "right";
                    }
                }

                $scope.clickHandlerAsset = function (event, asset) {
                    if (event.type == 'click') {
                        asset.compareVisible = !asset.compareVisible;
                        asset.zIndexTimeStamp = new Date().getTime();
                        if (asset.x < 250) {
                            asset.x = 250;
                        }
                        if (asset.s == 0) {
                            asset.s = 1;
                        }
                        asset.style = {
                            zIndex: asset.zIndex,
                            position: 'absolute',
                            transform: 'translate(' + asset.x + 'px, ' + asset.y + 'px) rotate(' + asset.r + 'deg) scale(' + asset.s + ')',
                            webkitTransform: 'translate(' + asset.x + 'px, ' + asset.y + 'px) rotate(' + asset.r + 'deg) scale(' + asset.s + ')'
                        }
                        if (asset.compareVisible) {
                            AssetLayerSystem.addAsset(asset);
                            $rootScope.$broadcast('compareVisibilityChange');
                        } else {
                            AssetLayerSystem.removeAsset(asset);
                            $rootScope.$broadcast('compareVisibilityChange');
                        }
                        
                        asset.$save();
                    }
                }


                interact('.assetRemovalDrop').dropzone({
                accept: '.gridCompareDraggable',
                    ondropactivate: function (event) {
                        event.target.classList.add('drop-active');
                    },
                    ondragenter: function (event) {
                        var draggableElement = event.relatedTarget,
                            dropzoneElement = event.target;
                        dropzoneElement.classList.add('drop-target');
                        draggableElement.classList.add('can-drop');
                    },
                    ondragleave: function (event) {
                        event.target.classList.remove('drop-target');
                        event.relatedTarget.classList.remove('can-drop');
                    },
                    ondrop: function (event) {
                        var asset = angular.element(event.relatedTarget).scope().asset;
                        asset.compareVisible = !asset.compareVisible;
                        asset.$save();
                    },
                    ondropdeactivate: function (event) {
                        event.target.classList.remove('drop-active');
                        event.target.classList.remove('drop-target');
                    }
                });

                $scope.GridsOpen = true;
                $scope.IspirationOpen = true;
                $scope.ProductsOpen = true;

                $scope.toggleAssetPanel = function () {
                    //Refersh cache from services before opening slider
                    loadData();

                    //Open Slider
                    $scope.showAssets = !$scope.showAssets;
                }

                $scope.$on('clearWorkspace', function (event, args) {
                    loadData();
                })

                initAssetPalette();
            }]
        };
    }]);
})();