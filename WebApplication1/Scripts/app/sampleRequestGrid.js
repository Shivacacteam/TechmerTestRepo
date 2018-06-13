(function () {
    var app = angular.module('requestGrid', ['techmerServices']);

    app.directive("requestGrid", ['Workspaces', 'Color', 'ColorPalette', 'Grid', 'Grids', 'ColorCell', function (Workspaces, Color, ColorPalette, Grid, Grids, ColorCell) {
        return {
            scope: {},
            restrict: 'E',
            templateUrl: "/partials/requestGrid.html",
            controller: ["$rootScope", "$scope", "$timeout", function ($rootScope, $scope, $timeout) {
                $scope.isOpen = true;

                //Required to prevent rzSlider from creating currentGrid in a lower scope.
                $scope.CurrentGrid = {
                    horizontalWeight: 0,
                    horizontalWeightView: 0,
                    verticalWeight: 0,
                    verticalWeightView: 0,
                    spacing: 0
                }
                //------------------------------------------------------------------------

                function loadGrids() {

                    Grids.list().then(function (grids) {

                        $scope.Grids = grids;
                        $scope.CurrentGrid = grids[0];
                    })
                }


                function initGradientGrid() {
                    Workspaces.currentWorkspace().then(function (result) {
                        $scope.Workspace = result;
                    })
                    Grids.list().then(function (grids) {
                        $scope.Grids = grids;
                        $scope.CurrentGrid = grids[0];


                        $scope.gridSpacingOptions = {
                            min: 0,
                            ceil: 16,
                            precision: 1,
                            hideLimitLabels: true,
                            showTicks: true,
                            showTicksValues: false,
                            onEnd: function (sliderId) {
                                $scope.UpdateImage($scope.CurrentGrid);
                            }
                        }

                        $scope.gridHorizontalWeightOptions = {
                            id: "hw",
                            min: 1,
                            ceil: 10,
                            precision: 1,
                            hideLimitLabels: true,
                            showTicks: true,
                            showTicksValues: false,
                            onChange: function () {
                                $scope.CurrentGrid.horizontalWeight = 2 - ($scope.CurrentGrid.horizontalWeightView * .2); //Invert Slider Oreintation
                                Grids.reCalcGridValues($scope.CurrentGrid)
                            },
                            onEnd: function (sliderId) {
                                $scope.UpdateImage($scope.CurrentGrid);
                            }
                        }

                        $scope.gridVerticalWeightOptions = {
                            id: "vw",
                            min: 1,
                            ceil: 10,
                            precision: 1,
                            vertical: true,
                            hideLimitLabels: true,
                            showTicks: true,
                            showTicksValues: false,
                            onChange: function () {
                                $scope.CurrentGrid.verticalWeight = $scope.CurrentGrid.verticalWeightView * .2;
                                Grids.reCalcGridValues($scope.CurrentGrid)
                            },
                            onEnd: function (sliderId) {
                                $scope.UpdateImage($scope.CurrentGrid);
                            }
                        }
                        /*
                        $scope.$watch(function ($scope) { return $scope.CurrentGrid.horizontalWeight }, function (newVal, oldVal) {
                            if (!newVal) return;
                            if (newVal == oldVal) return;
                            if (!$scope.CurrentGrid.gridArray) return;
                            Grids.reCalcGridValues($scope.CurrentGrid);
                        });
                        */
                        $scope.$watch(function ($scope) { return $scope.CurrentGrid.verticalWeight }, function (newVal, oldVal) {
                            if (!newVal) return;
                            if (newVal == oldVal) return;
                            if (!$scope.CurrentGrid.gridArray) return;
                            Grids.reCalcGridValues($scope.CurrentGrid);
                        });


                    });
                }


                $scope.setGrid = function (grid) {
                    var IsNew = true;
                    angular.forEach($rootScope.SampleRequest.SelectedProduct, function (value) {
                        if (value.assetId == grid.id) {
                            IsNew = false;
                        }
                    });

                    if (IsNew) {
                        $scope.SampleGridData = [];
                        $scope.SampleGridData.assetId = grid.id;
                        $scope.SampleGridData.assetType = 'Grid';
                        $scope.SampleGridData.assetTitle = 'Color Dicision';
                        $scope.SampleGridData.assetbackground = grid.image;
                        $rootScope.SampleRequest.SelectedProduct.push($scope.SampleGridData);
                    }
                    else {
                        Alert($("#SampleRequest"), 2, '! Seeding request already exists.');
                    }
                }

                $scope.$on('GridSelectionChange', function (event, args) {
                    loadGrids();
                })

                initGradientGrid();

            }] //End Controller
        }; //End Return (gradientGrid)
    }]); //End Directive (gradientGrid)
})(); //End File
