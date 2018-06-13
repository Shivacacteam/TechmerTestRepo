(function () {
    var app = angular.module('gridPalette', ['TechmerVision', 'techmerServices']);

    app.directive("gridPalette", ['Grids', function (Grids) {
        return {
            restrict: 'E',
            templateUrl: "/partials/gridPalette.html",
            controller: ["$rootScope", "$scope", "$element", "$attrs", function ($rootScope,$scope, $element, $attrs) {
                $scope.showGrids = false;
                function loadPalette(){
                    Grids.list().then(function (grids) {
                        $scope.Grids = grids;

                    })
                }

                function initGridPalette() {
                    $scope.showGrids = false;
                    if ($attrs.psSide) {
                        $scope.slideDirection = $attrs.psSide;
                    }
                    else {
                        $scope.slideDirection = "right";
                    }
                }

                $scope.setCurrentGrid = function (grid, event) {
                    if (event.type == "click") {
                        Grids.selectGrid(grid);
                        $rootScope.$broadcast("GridSelectionChange");
                    }
                }

                $scope.deleteGrid = function (grid, event) {
                    if (event.type == "click") {
                        Grids.deleteGrid(grid);
                        loadPalette();
                        $rootScope.$broadcast("GridSelectionChange");
                    }
                }

                $scope.toggleGridsPanel = function () {
                    
                    $scope.showGrids = !$scope.showGrids;
                }

                $scope.$on('toggleGridsPanel', function (event, args) {
                    $scope.toggleGridsPanel();
                })

                $scope.$on('clearWorkspace', function (event, args) {
                    loadPalette();
                })
                $scope.$on('GridsChanged', function (event, args) {
                    loadPalette();
                })

                initGridPalette();
                loadPalette();
                
            }]
        };
    }]);
})();