(function(){
    var app = angular.module('gradientGrid', ['techmerServices']);     

	app.directive("gradientGrid", ['Workspaces', 'Color','ColorPalette', 'Grid','Grids','ColorCell',function(Workspaces, Color,ColorPalette,Grid,Grids,ColorCell) {
	  return {
	    scope: {},
	    restrict: 'E',
	    templateUrl: "/partials/gradientGrid.html",
	    controller: ["$rootScope", "$scope", "$timeout", function ($rootScope, $scope, $timeout) {
	        $scope.isOpen = true;

            //Required to prevent rzSlider from creating currentGrid in a lower scope.
	        $scope.CurrentGrid = {
	            horizontalWeight:0,
                horizontalWeightView:0,
                verticalWeight:0,
                verticalWeightView:0,
	            spacing:0
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

            interact('.colorCellDrop').dropzone({
                    accept: '.colorDraggable',
                    overlap: 0.1,
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
                        var cell = angular.element(event.target).scope().cell;
                        var colorData = event.relatedTarget.getAttribute('data-color').toString().replace("[", "").replace("]", "").split(",");
                        colorData[0] = parseInt(colorData[0]);
                        colorData[1] = parseInt(colorData[1]);
                        colorData[2] = parseInt(colorData[2]);
                        colorData[3] = parseInt(colorData[3]);
                        $scope.droppedCell(cell, colorData);
                        $scope.$apply();
                        $scope.UpdateImage($scope.CurrentGrid);
                    },
                    ondropdeactivate: function (event) {
                        event.target.classList.remove('drop-active');
                        event.target.classList.remove('drop-target');
                    }
            });
            interact('.colorCellTap').on('tap', function (event) {
                var cell = angular.element(event.target).scope().cell;
                $scope.clickedCell(cell);
                $scope.$apply();
                $scope.UpdateImage($scope.CurrentGrid);
                event.preventDefault();
            });

            $scope.droppedCell = function (cell, colorData) {
                if (cell.paintable) {
                    $scope.paintCell(cell, colorData);
                }
            };

            $scope.clickedCell = function(cell){
                if(cell.paintable){
                    ColorPalette.currentColor().then(function(result){
                        $scope.paintCell(cell, result.colorData);
                    });
                }
                else{
                    ColorPalette.addColor(cell.colorData, $scope.Workspace.id);
                }
            };
            

            $scope.paintCell = function (cell, colorData) {
                if (cell.paintable) {
                    var newCell = ColorCell.buildCell(colorData, cell.paintable);
                    angular.copy(newCell, cell);
                    Grids.reCalcGridValues($scope.CurrentGrid);
                    $scope.CurrentGrid.topLeftColorData = $scope.CurrentGrid.gridArray[0][0].colorData;
                    $scope.CurrentGrid.topRightColorData = $scope.CurrentGrid.gridArray[0][$scope.CurrentGrid.width - 1].colorData;
                    $scope.CurrentGrid.bottomLeftColorData = $scope.CurrentGrid.gridArray[$scope.CurrentGrid.height - 1][0].colorData;
                    $scope.CurrentGrid.bottomRightColorData = $scope.CurrentGrid.gridArray[$scope.CurrentGrid.height - 1][$scope.CurrentGrid.width - 1].colorData;
                   
                }
            };

            $scope.saveGrid = function (grid) {
                $scope.UpdateImage($scope.CurrentGrid);
            };

            $scope.UpdateImage = function (currentGrid) {
                var dpiMultipler = 4;
                var div = document.getElementById('gradientGrid');
                var newDiv = div.cloneNode(true);
                newDiv.setAttribute("id", "hiDpiGrid");
                newDiv.style.transform = newDiv.style.transform.concat("scale(", dpiMultipler, ",", dpiMultipler, ")");

                var renderWidth = div.clientWidth * dpiMultipler;
                var renderHeight = div.clientHeight * dpiMultipler;


                newDiv.style.display = "none";
                newDiv.style.top = "550px";
                newDiv.style.left = "550px";
                newDiv.style.position = "absolute";

                document.body.appendChild(newDiv);
                
                html2canvas(newDiv,{
                    width: renderWidth,
                    height: renderHeight,
                    useCORS: true,
                    //proxy: "/proxy/html2canvasproxy.ashx",
                    //onrendered: function (canvas) {
                }).then(function(canvas){
                        document.body.removeChild(newDiv);
                        //document.body.appendChild(canvas);
                        currentGrid.image = canvas.toDataURL('image/png');
                        currentGrid.$save().then(function (resut) {
                            $rootScope.$broadcast('GridsChanged');
                            //$scope.$apply();
                        })
                        
                    }
                )
            }

            $scope.exportGrid = function (event) {
                Grids.downloadGrid(event);
            }

            $scope.clearGrid = function (grid) {
                Grids.deleteGrid(grid)
                Grids.newGrid($scope.Workspace.id);
                $rootScope.$broadcast('GridsChanged');
            }

            $scope.newGrid = function () {
                Grids.newGrid($scope.Workspace.id).then(function (result) {
                    $scope.UpdateImage(result);
                });
                $rootScope.$broadcast('GridSelectionChange');

            }

            $scope.copyGrid = function (grid) {
                Grids.copyGrid(grid).then(function (results) {
                    $rootScope.$broadcast('GridsChanged');
                })
            }

            $scope.setShape = function (borderRadius) {
                Grids.setBorderRadius($scope.CurrentGrid, borderRadius).then(function(response){
                    $scope.CurrentGrid = response;
                    $scope.UpdateImage($scope.CurrentGrid);
                })
            }

            $scope.showAllClickHandler = function () {
                $rootScope.$broadcast('toggleGridsPanel');
            }

            $scope.$on('clearWorkspace', function (event, args){
                Grids.clearGrids(args.workspace).then(function (result) {
                    $scope.Grids = [result];
                    $scope.CurrentGrid = result;
                    $scope.saveGrid($scope.CurrentGrid);//Update Thumbnail
                })
            });

            $timeout(function () {
                $scope.$broadcast('rzSliderForceRender');
                $scope.$applyAsync();
            });
                
            $scope.$on('GridSelectionChange', function (event, args) {
                loadGrids();
            })

            initGradientGrid();

	    }] //End Controller
	  }; //End Return (gradientGrid)
	}]); //End Directive (gradientGrid)
})(); //End File
