(function () {
    var app = angular.module('favoriteColors', ['techmerServices']);

    app.directive("favoriteColors", ['FavoriteColorPalette', 'Color', function (FavoriteColorPalette, Color) {
        return {
            scope: {},
            restrict: 'E',
            templateUrl: "/partials/favoritecolor.html",
            controller: ["$rootScope", "$scope", function ($rootScope, $scope) {
                $scope.isOpen = true;

                function loadFavoriteColors() {

                    FavoriteColorPalette.list().then(function (colors) {

                        $scope.favoriteColors = colors;
                    })
                }

                interact('.colorFavoriteDrop').dropzone({
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

                        var favColor = angular.element(event.target).scope().favColor;
                        var asset2 = angular.element(event.relatedTarget).scope();
                        var colorData = event.relatedTarget.getAttribute('data-color').toString().replace("[", "").replace("]", "").split(",");
                        colorData[0] = parseInt(colorData[0]);
                        colorData[1] = parseInt(colorData[1]);
                        colorData[2] = parseInt(colorData[2]);
                        colorData[3] = parseInt(colorData[3]);
                        $scope.setFavoriteColor(colorData, favColor);

                    },
                    ondropdeactivate: function (event) {
                        event.target.classList.remove('drop-active');
                        event.target.classList.remove('drop-target');
                    }
                });

                $scope.setFavoriteColor = function (colorData, favColor) {

                    FavoriteColorPalette.setColor(colorData, favColor);

                }

                $scope.deleteFavoriteColor = function (favColor) {
                    FavoriteColorPalette.setColor([255, 255, 255], favColor);
                }


                $scope.setSamplerequest = function (favColor) {
                    var IsNew = true;
                    angular.forEach($rootScope.SampleRequest.SelectedProduct, function (value) {
                        if (value.assetId == favColor.id) {
                            IsNew = false;
                        }
                    });

                    if (IsNew) {
                        $scope.SampleColorData = [];
                        $scope.SampleColorData.assetId = favColor.id;
                        $scope.SampleColorData.assetType = 'Color';
                        $scope.SampleColorData.assetTitle = 'fav Color';
                        $scope.SampleColorData.assetbackground = favColor.colorStyle;

                        $rootScope.SampleRequest.SelectedProduct.push($scope.SampleColorData);
                    }
                    else {
                        Alert($("#SampleRequest"), 2, '! Seeding request already exists.');
                    }

                 
                }
                $scope.$on('clearWorkspace', function (event, args) {
                    FavoriteColorPalette.clearFavoriteColors(args.workspace);
                })

                loadFavoriteColors();

            }] //End Controller
        }; //End Return (favoriteColors)
    }]); //End Directive (favoriteColors)
})(); //End File
