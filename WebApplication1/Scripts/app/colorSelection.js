(function(){
    var app = angular.module('colorSelection', ['techmerServices','colorProvider']);


	app.directive("colorSelection", ['ColorPalette', 'Workspaces', function(ColorPalette, Workspaces) {
	    return {
	    scope: {},
	    restrict: 'E',
	    templateUrl: "/partials/colorSelection.html",
	    controller: ["$rootScope", "$scope", function ($rootScope, $scope) {
	        $scope.isOpen = true;
	        
	        $scope.selectedColor = {
	            colorData: [0, 0, 0, 1],
	            LAB: [0, 0, 0]
	        }

	        function loadWorkspace() {
	            Workspaces.currentWorkspace().then(function(workspaces){
	                $scope.currentWorkspace = workspaces;
	            });
	        }
	        function loadColors() {
	          
		        ColorPalette.list().then(function (colors) {
		            $scope.selectedColor = colors[0];
		            //$scope.selectedColor.LAB = colors[0].LAB[0];
		            $scope.colorSelections = colors;
		        })
		    }

		    interact('.colorDraggable')
    .draggable({
        autoScroll: true,
        onstart: function (event) {
            var target = event.target,
                x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
                y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

            target.setAttribute('data-origx', x);
            target.setAttribute('data-origy', y);
            target.classList.add("colorDragActive");

        },
        onmove: function (event) {
            var target = event.target,
                // keep the dragged position in the data-x/data-y attributes
                x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
                y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

            // translate the element
            target.style.webkitTransform =
            target.style.transform =
              'translate(' + x + 'px, ' + y + 'px)';

            // update the position attributes
            target.setAttribute('data-x', x);
            target.setAttribute('data-y', y);
        },
        onend: function (event) {
            var target = event.target,
               x = (parseFloat(target.getAttribute('data-origx')) || 0),
               y = (parseFloat(target.getAttribute('data-origy')) || 0);

            // translate the element
            target.style.webkitTransform =
            target.style.transform =
              'translate(' + x + 'px, ' + y + 'px)';

            // update the position attributes
            target.setAttribute('data-x', x);
            target.setAttribute('data-y', y);
            target.classList.remove("colorDragActive");
            event.preventDefault();
            event.stopPropagation();
        }
    })

	interact('.colorTapable')
    .on('tap', function (event) {
    
        var colorData = event.currentTarget.getAttribute('data-color').toString().replace("[", "").replace("]", "").split(",");
        ColorPalette.addColor(colorData, $scope.currentWorkspace.id).then();
        event.preventDefault();
    });

            interact('.colorSelectionDrop').dropzone({
                accept: '.colorDraggable',
                overlap: 0.50,
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
                    var colorData = event.relatedTarget.getAttribute('data-color').toString().replace("[", "").replace("]", "").split(",");
                    $scope.setSelectedColor(colorData);
                },
                ondropdeactivate: function (event) {
                    event.target.classList.remove('drop-active');
                    event.target.classList.remove('drop-target');
                }
            });

            $scope.setSelectedColor = function (colorData) {
             
			    ColorPalette.addColor(colorData, $scope.currentWorkspace.id).then(function (result) {
			        $rootScope.$broadcast('ColorSelectionChanged');

			    });
			}

            $scope.$on('clearWorkspace', function (event, args){
                ColorPalette.clearColorSelections(args.workspace).then(function (result) {
                    $scope.selectedColor = result[0];
                    $scope.colorSelections = result;
                })
            })
            $scope.$on('ColorSelectionChanged', function (event, args) {
                loadColors();
            })

            loadWorkspace();
            loadColors();
		}]//End Controller
	  }; //End Return (colorSelection)
	}]); //End Directive (colorSelection)
})(); //End File
