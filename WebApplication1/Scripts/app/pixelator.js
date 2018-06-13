(function(){
    var app = angular.module('pixelator', ["techmerServices"]);

    app.directive("pixelator", function() {
        return {
            scope: {},
            restrict: 'E',
            templateUrl: "/partials/pixelator.html",
            controller: ["$rootScope", "$scope", "$element", "$timeout", "ColorPalette", "Workspaces", function ($rootScope, $scope, $element, $timeout, ColorPalette, Workspaces) {

            $scope.isOpen = true;

            $scope.currentWorkspace = {
                pixelation: 1
            }
            
            $scope.pixelationOptions = {
                        id: "pix",
                        min: 1,
                        ceil: 10,
                        hideLimitLabels: true,
                        showTicks: true,
                        showTicksValues: false,
                        onChange: function () {
                            pixelateImage();
                        },
                        onEnd: function (sliderId) {
                            $scope.currentWorkspace.$save();
                        }
                    }

                function loadWorkspace(){
                    Workspaces.list().then(function (workspaces) {
                        $scope.currentWorkspace = workspaces[0];
                        pixelateImage();
                    })
                }


                function pixelateImage() {
                    var tempCanvas = document.getElementById('pixCan');
                    if (tempCanvas) {
                        ctx = tempCanvas.getContext('2d');

                        if ($scope.currentWorkspace.image == "") {
                            ctx.clearRect(0, 0, tempCanvas.width, tempCanvas.height)
                        }
                        else {
                            /// draw original image to the scaled size
                            var img = document.createElement('img');
                            if ($scope.currentWorkspace.image.substr(0,4) == 'http')
                            {
                                img.crossOrigin = "Anonymous";
                            }
                            img.src = $scope.currentWorkspace.image;
                            img.onload = function () {
                                var aspectRatio = img.height / img.width;

                                tempCanvas.width = tempCanvas.clientWidth;
                                tempCanvas.height = tempCanvas.width * aspectRatio;
                                tempCanvas.clientHeight = tempCanvas.height;

                                var size = $scope.currentWorkspace.pixelation * 0.01,
                                /// cache scaled width and height
                                    w = tempCanvas.width * size,
                                    h = tempCanvas.height * size;

                                ctx.mozImageSmoothingEnabled = false;
                                ctx.imageSmoothingEnabled = false;
                                ctx.webkitImageSmoothingEnabled = false;
                                ctx.msImageSmoothingEnabled = false;


                                ctx.drawImage(img, 0, 0, w, h);


                                /// then draw that scaled image thumb back to fill canvas
                                /// As smoothing is off the result will be pixelated
                                ctx.drawImage(tempCanvas, 0, 0, w, h, 0, 0, tempCanvas.width, tempCanvas.height);
                                $scope.$applyAsync();
                            }
                        }
                    }
                };

                $scope.changePixelation = function (event, workspace) {
                    pixelateImage();
                    workspace.$save();
                };

                $scope.getClickColor = function (event) {
                    var ctx = event.currentTarget.getContext('2d');

                    if(event.type == "click"){
                        var x = event.offsetX;
                        var y = event.offsetY;
                    } else if (event.type == "touchend") {
                        var rect = event.target.getBoundingClientRect();
                        var x = event.originalEvent.changedTouches[0].pageX - rect.left;
                        var y = event.originalEvent.changedTouches[0].pageY - rect.top;
                    }

                    
                    
                    var imageData = ctx.getImageData(x, y, 1, 1);
                    var ret = [imageData.data[0], imageData.data[1], imageData.data[2], 1];

                    if ($scope.currentWorkspace.image) {
                        ColorPalette.addColor(ret, $scope.currentWorkspace.id).then(function(result){
                            $rootScope.$broadcast('ColorSelectionChanged');
                        })
                        
                    }
                };

                $scope.$on('clearWorkspace', function (event, args){
                    loadWorkspace();
                });

                $scope.$on('InspirationChanged', function (event, args) {
                    loadWorkspace();
                });

                function initPixelator() {
                    $scope.currentWorkspace.pixelation = 1;
                };
                
                $timeout(function () {
                    loadWorkspace(); //After Route Change: Delay pixelation until DOM is loaded.
                })
                
            }]
        };
    });
})();