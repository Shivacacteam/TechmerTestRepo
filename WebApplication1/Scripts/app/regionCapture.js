var app = angular.module('techmerRegionCapture', ['techmerServices']);

app.directive("regionCapture", function () {
    return {
        restrict: 'E',
        templateUrl: "/partials/regionCapture.html",
        controller: ["$scope", "$document", function ($scope, $document) {
            $scope.regionCapture = {
                x: 0,
                y: 0,
                height: 200,
                width: 200,
                visible: false
            }

            $scope.clickHandler = function ($event) {
                if ($scope.regionCapture.visible) {
                    var dpiMultipler = 2;
                    var div = document.getElementById('gridCompareContainer');
                    var newDiv = div.cloneNode(true);
                    newDiv.style.transform = newDiv.style.transform.concat("scale(", dpiMultipler, ",", dpiMultipler, ")");

                    var renderWidth = div.clientWidth * dpiMultipler;
                    var renderHeight = div.clientHeight * dpiMultipler;

                    newDiv.classList.add("clearfix");
                    newDiv.style.top = "5000px";
                    newDiv.style.left = "550px";
                    newDiv.style.position = "absolute";
                    document.body.appendChild(newDiv);
                    var html2canvasOptions = {
                        width: renderWidth,
                        height: renderHeight,
                        javascripEnabled: true,
                        logging: false,
                        useCORS: true,
                        //proxy: "/proxy/html2canvasproxy.ashx"
                    }

                    html2canvas(newDiv, html2canvasOptions ).then(function(canvas){
                            document.body.removeChild(newDiv);
                            //document.body.appendChild(canvas);
                            
                            var logo = new Image();
                            logo.src = "/images/TechmerVisionLogo-md.png";
                            logo.onload = function () {
                                var gridCompareImage = new Image();
                                gridCompareImage.src = canvas.toDataURL('image/png');
                                gridCompareImage.onload = function () {
                                    var whiteFrameSize = 5;
                                    var blackFrameSize = 3;
                                    var padding = 2;
                                    var totalFrame = (whiteFrameSize + blackFrameSize + padding);
                                    var logoSize = 100;
                                    var viewFinder = {
                                        x: $scope.regionCapture.x * dpiMultipler,
                                        y: $scope.regionCapture.y * dpiMultipler,
                                        width: $scope.regionCapture.width * dpiMultipler,
                                        height: $scope.regionCapture.height * dpiMultipler
                                    }

                                    var can = document.createElement('canvas');
                                    if (viewFinder.width + (totalFrame * 2) < 370) {
                                        //Minimum Export Size
                                        can.width = 370;
                                    } else {
                                        can.width = viewFinder.width + (totalFrame * 2);
                                    }
                                    can.height = viewFinder.height + logoSize + (totalFrame * 2);
                                    var localctx = can.getContext('2d');

                                    //White Background
                                    localctx.fillStyle = "white";
                                    localctx.rect(0, 0, can.width, can.height);
                                    localctx.fill();

                                    //Black Frame
                                    localctx.beginPath();
                                    localctx.strokeStyle = "black";
                                    localctx.lineWidth = blackFrameSize;
                                    localctx.rect(whiteFrameSize, whiteFrameSize, can.width - totalFrame, can.height - totalFrame);
                                    localctx.stroke();

                                    //Draw captured image
                                    var xDrawPosition = ((can.width / 2) - (viewFinder.width / 2));
                                    localctx.drawImage(gridCompareImage, viewFinder.x, viewFinder.y, viewFinder.width, viewFinder.height, xDrawPosition, totalFrame, viewFinder.width, viewFinder.height);

                                    //Paint Logo
                                    localctx.drawImage(logo, can.width - 150, can.height - totalFrame - 71, 150, 71);

                                    //Paint Contact Info
                                    var textSize = 25;
                                    var textPadding = 4;
                                    localctx.font = "bold " + textSize + "px Arial";
                                    localctx.fillStyle = "black";
                                    localctx.fillText("Techmer PM", totalFrame + textPadding, can.height - logoSize - totalFrame + (textSize * 1) + textPadding);
                                    textSize = 15;
                                    localctx.font = textSize + "px Arial";
                                    localctx.fillText("www.techmerpm.com", totalFrame + textPadding, can.height - logoSize - totalFrame + (textSize * 3));
                                    localctx.fillText("web@techmerpm.com", totalFrame + textPadding, can.height - logoSize - totalFrame + (textSize * 4));

                                    can.classList.add("clearfix");
                                    can.style.top = "5000px";
                                    can.style.left = "550px";
                                    can.style.position = "absolute";

                                    //document.appendChild(can);

                                    var imgLarge = new Image();
                                    imgLarge.src = can.toDataURL('image/png');
                                    imgLarge.onload = function () {
                                        //window.open(imgLarge.src.replace(/^data:image\/[^;]/, 'data:application/octet-stream'));
                                        
                                        var link = document.createElement('a');
                                        link.href = imgLarge.src;
                                        link.download = 'regionCapture.png';
                                        link.className + " ng-hide";
                                        document.body.appendChild(link);
                                        link.click();
                                        
                                    }
                                }
                            }
                    })
                } 
                $scope.regionCapture.visible = !$scope.regionCapture.visible;
            }

            interact('.regionCaptureDraggable')
                .draggable({
                    inertia: false,
                    restrict: {
                        restriction: '.gridCompareContainer',
                        endonly: true
                    },
                    autoScroll: false,
                    onstart: onStartListener,
                    onmove: onMoveListener
                })
                .gesturable({
                    onstart:onStartListener,
                    onmove: onMoveListener
                })
                .resizable({
                    preserveAspectRatio: false,
                    edges: { left: true, right: true, bottom: true, top: true }
                })
                .on('resizemove', function (event) {
                    var target = event.target,
                        x = (parseFloat(target.getAttribute('data-x')) || 0),
                        y = (parseFloat(target.getAttribute('data-y')) || 0);

                    // update the element's style
                    target.setAttribute('width', event.rect.width + 'px');
                    target.setAttribute('height', event.rect.height + 'px');

                    // translate when resizing from top or left edges
                    x += event.deltaRect.left;
                    y += event.deltaRect.top;

                    target.style.webkitTransform = target.style.transform =
                        'translate(' + x + 'px,' + y + 'px)';

                    target.setAttribute('data-x', x);
                    target.setAttribute('data-y', y);
                    target.firstElementChild.setAttribute("height", (event.rect.height - 10) + 'px');
                    target.firstElementChild.setAttribute("width", (event.rect.width - 10) + 'px');
                    $scope.regionCapture.x = x;
                    $scope.regionCapture.y = y;
                    $scope.regionCapture.height = (event.rect.height);
                    $scope.regionCapture.width = (event.rect.width);
                    //target.textContent = Math.round(event.rect.width) + '×' + Math.round(event.rect.height);
                });

            function onMoveListener(event) {
                var target = event.target,
                    x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
                    y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy,
                    s = (parseFloat(target.getAttribute('data-s')) || 1);

                if (!isNaN(event.ds)) {
                    s = (1 + event.ds);
                    /*
                    s = s * (1 + event.ds);
                    if (s < .25) {
                        s = .25;
                    }
                    */
                }

                if (s != 1) {
                    var a = 1;
                }

                target.style.webkitTransform = target.style.transform = 'translate(' + x + 'px, ' + y + 'px)';
                
                $scope.regionCapture.height = ($scope.regionCapture.height * s);
                $scope.regionCapture.width = ($scope.regionCapture.width * s);
                target.setAttribute('data-x', x);
                target.setAttribute('data-y', y);
                target.setAttribute("height", ($scope.regionCapture.height) + "px");
                target.setAttribute("width", ($scope.regionCapture.width) + "px");
                //target.setAttribute('data-s', s);
                target.firstElementChild.setAttribute("height", ($scope.regionCapture.height - 10) + 'px');
                target.firstElementChild.setAttribute("width", ($scope.regionCapture.width - 10) + 'px');
                $scope.regionCapture.x = x;
                $scope.regionCapture.y = y;
                

            }

            function onStartListener(event) {
                //event.target.parent.append(event.target);
                //var grid = $scope.Grids[event.target.getAttribute('data-index')];
                //grid.zIndex = $scope.zIndex;
                $scope.zIndex = 9000;
                $scope.$applyAsync();
                
            }

        }]
    }
});