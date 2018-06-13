/**
 * Created by aedmonds on 8/13/2015.
 */
(function(){
    var app = angular.module('reColor', ['techmerServices']);

    app.directive("reColor", ["ColorPalette", "ProductPalette", function(ColorPalette, ProductPalette) {
        return {
            scope: {},
            restrict: 'E',
            templateUrl: "/partials/reColor.html",
            link: function ($scope, $element) {
                $scope.isOpen = true;
                function loadProducts() {
                    ProductPalette.list().then(
                        function (products) {
                            $scope.CurrentProduct = products[0];                      
                        }
                    )
                }

                $scope.colorStyle = function (color) {
                    
                    var ret = 'rgba(' + color.colorData + ')'
                    return ret;
                }

                $scope.drawImage = function () {
                    var canvas = $element.find('canvas')[0];
                    var ctx = canvas.getContext("2d");
                    ctx.clearRect(0, 0, 550, 550);
                    ctx.drawImage($scope.reColorImg, 0, 0, 550, 550);
                }

                interact('.reColorCellDrop').dropzone({
                    
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
                    var Index = parseInt(event.target.getAttribute('data-index') || 0);
                    colorData[0] = parseInt(colorData[0]);
                    colorData[1] = parseInt(colorData[1]);
                    colorData[2] = parseInt(colorData[2]);
                    colorData[3] = parseInt(colorData[3]);
                    //$scope.reColor(Index, colorData);
                    $scope.reColorV2(Index, colorData);
                },
                ondropdeactivate: function (event) {
                    event.target.classList.remove('drop-active');
                    event.target.classList.remove('drop-target');
                }
                });
                interact('.reColorCellTap').on('tap', function (event) {
                  
                    var Index = parseInt(event.target.getAttribute('data-index') || 0);
                    $scope.clickedReColorCell(Index);
                    event.preventDefault();
                });

                $scope.clickedReColorCell = function (index) {
                  
                    ColorPalette.currentColor().then(function (result) {
                        $scope.reColorV2(index, result.colorData);
                    })
                }

                $scope.reColor = function ($index, colorData) {
                  
                    //Get Canvas and Context
                    var canvas = $element.find('canvas')[0];
                    var ctx = canvas.getContext('2d');

                    //Update Product Color
                    $scope.CurrentProduct.productColors[$index].internalColorString = colorData.join(",");
                    $scope.CurrentProduct.productColors[$index].colorData = colorData;
                    var productColors = $scope.CurrentProduct.productColors;
                    
                    //Get Produt Template Colors
                    var productTemplateColors = $scope.CurrentProduct.productTemplate.productTemplateColors;

                    //Prepare Canvas for drawing
                    var productTemplateImage = new Image();
                    if ($scope.CurrentProduct.productTemplate.image.substring(0, 4) == "http") {
                        productTemplateImage.crossOrigin = "anonymous";
                    }
                    productTemplateImage.src = $scope.CurrentProduct.productTemplate.image;
                    productTemplateImage.onload = function(){
                        canvas.height = productTemplateImage.height;
                        canvas.width = productTemplateImage.width;
                        ctx.clearRect(0, 0, canvas.width, canvas.height);
                        ctx.drawImage(productTemplateImage, 0, 0, canvas.width, canvas.height);
                        var imgData = ctx.getImageData(0, 0, canvas.width, canvas.height);
                        var data = imgData.data;

                        //Process Image Pixels
                        for (var i = 0; i < data.length; i += 4) {
                            red = data[i + 0];
                            green = data[i + 1];
                            blue = data[i + 2];
                            alpha = data[i + 3];

                            // skip transparent/semiTransparent pixels
                            if (alpha < 200) {
                                continue;
                            }

                            //Calculate Hue of Pixels
                            var pixelhsl = rgbToHsl(red, green, blue);
                            var pixelhue = (pixelhsl.h * 360)%360;

                            for (j = 0; j < productTemplateColors.length; j++ ){
                                if ((pixelhue >= productTemplateColors[j].recolorToleranceLowerLimit && pixelhue <= productTemplateColors[j].recolorToleranceUpperLimit) ||
                                    (pixelhue-360 >= productTemplateColors[j].recolorToleranceLowerLimit && pixelhue-360 <= productTemplateColors[j].recolorToleranceUpperLimit)) {
                                    //Calulate difference between base color
                                    data[i + 0] -= (productTemplateColors[j].colorData[0] - productColors[j].colorData[0]);
                                    data[i + 1] -= (productTemplateColors[j].colorData[1] - productColors[j].colorData[1]);
                                    data[i + 2] -= (productTemplateColors[j].colorData[2] - productColors[j].colorData[2]);
                                    data[i + 3] = 255;
                                    break;
                                }
                            }
                        }
                        //Draw to Canvas
                        ctx.putImageData(imgData, 0, 0);

                        if ($scope.CurrentProduct.hasBackgroundImage) {
                            var productTemplateBackgroundImage = new Image();
                            productTemplateBackgroundImage.src = $scope.CurrentProduct.backgroundImage;
                            productTemplateBackgroundImage.onload = function () {
                                var recoloredImage = new Image();
                                recoloredImage.src = canvas.toDataURL("image/png");
                                recoloredImage.onload = function () {
                                    ctx.clearRect(0, 0, canvas.width, canvas.height);
                                    ctx.drawImage(productTemplateBackgroundImage, 0, 0, canvas.width, canvas.height);
                                    ctx.drawImage(recoloredImage, 0, 0, canvas.width, canvas.height)
                                    updateProductImage();
                                }
                            }
                        } else {
                            updateProductImage();
                        }

                        function updateProductImage() {
                            //Update Product Image and save
                            $scope.CurrentProduct.image = canvas.toDataURL("image/png");
                            $scope.CurrentProduct.$save(function (data) {
                                $scope.$applyAsync();
                            });
                        }
                    }
                }

                $scope.reColorV2 = function ($index, colorData) {
                    
                    //Update Product Color
                    $scope.CurrentProduct.productColors[$index].internalColorString = colorData.join(",");
                    $scope.CurrentProduct.productColors[$index].colorData = colorData;
                    var productColors = $scope.CurrentProduct.productColors;

                    //Get Product Template Colors
                    var productTemplateColors = $scope.CurrentProduct.productTemplate.productTemplateColors;

                    //Cycle All Layers

                        var productTemplateColorImage = new Image();
                        if ($scope.CurrentProduct.productTemplate.productTemplateColors[$index].image.substring(0, 4) == "http") {
                            productTemplateColorImage.crossOrigin = "anonymous";
                        }
                        productTemplateColorImage.onload = function () {
                            //Get Canvas and Context
                            var canvas = $element.find('canvas')[0];
                            var ctx = canvas.getContext('2d');
                            canvas.height = productTemplateColorImage.height;
                            canvas.width = productTemplateColorImage.width;
                            ctx.clearRect(0, 0, canvas.width, canvas.height);
                            ctx.drawImage(productTemplateColorImage, 0, 0, canvas.width, canvas.height);
                            var imgData = ctx.getImageData(0, 0, canvas.width, canvas.height);
                            var data = imgData.data;

                            //Process Image Pixels
                            for (var i = 0; i < data.length; i += 4) {
                                red = data[i + 0];
                                green = data[i + 1];
                                blue = data[i + 2];
                                alpha = data[i + 3];

                                // skip transparent/semiTransparent pixels
                                if (alpha < 200) {
                                    continue;
                                }

                                //Calculate Hue of Pixels
                                var pixelhsl = rgbToHsl(red, green, blue);
                                var pixelhue = (pixelhsl.h * 360) % 360;

                                if ((pixelhue >= productTemplateColors[$index].recolorToleranceLowerLimit && pixelhue <= productTemplateColors[$index].recolorToleranceUpperLimit) ||
                                    (pixelhue - 360 >= productTemplateColors[$index].recolorToleranceLowerLimit && pixelhue - 360 <= productTemplateColors[$index].recolorToleranceUpperLimit)) {
                                    //Calulate difference between base color
                                    data[i + 0] -= (productTemplateColors[$index].colorData[0] - productColors[$index].colorData[0]);
                                    data[i + 1] -= (productTemplateColors[$index].colorData[1] - productColors[$index].colorData[1]);
                                    data[i + 2] -= (productTemplateColors[$index].colorData[2] - productColors[$index].colorData[2]);
                                    data[i + 3] = 255;
                                    //break;
                                }
                            } //Process Pixels
                            //Draw to Canvas
                            ctx.putImageData(imgData, 0, 0);

                            $scope.CurrentProduct.productColors[$index].image = canvas.toDataURL("image/png");
                            $scope.updateProductImage($element);
                        } //ImageLoad
                        productTemplateColorImage.src = $scope.CurrentProduct.productTemplate.productTemplateColors[$index].image;


                }//RecolorV2

                $scope.updateProductImage = function (element) {
                    debugger;
                    var totalImageCount = 0;

                    //load each productColor Image
                    var cp = $scope.CurrentProduct;
                    if(cp.hasBackgroundImage){
                        totalImageCount++;
                        cp.htmlImage = new Image();
                        if (cp.image.substring(0, 4) == 'http') {
                            cp.htmlImage.crossOrigin = 'anonymous';
                        }
                        cp.htmlImage.onload = function () {
                            totalImageCount--;
                            if (!totalImageCount) {
                                $scope.paintAll();
                            }
                        }
                        cp.htmlImage.src = cp.image;

                    }

                    var pcs = $scope.CurrentProduct.productColors
                    for (i = 0; i < pcs.length; i++) {
                        totalImageCount++
                        pcs[i].htmlImage = new Image();
                        if (pcs[i].image.substring(0, 4) == 'http') {
                            pcs[i].htmlImage.crossOrigin = 'anonymous';
                        }
                        pcs[i].htmlImage.onload = function () {
                            totalImageCount--;
                            if (!totalImageCount) {
                                $scope.paintAll();
                            }
                        };
                        pcs[i].htmlImage.src = pcs[i].image;

                    }
                    $scope.paintAll = function () {
                      
                        var canvas = element.find('canvas')[0];
                        var ctx = canvas.getContext('2d');
                        canvas.height = pcs[0].htmlImage.height;
                        canvas.width = pcs[0].htmlImage.width;
                        ctx.clearRect(0, 0, canvas.width, canvas.height);

                        //If (backgroundNeeded){
                        //      paintBackground
                        //}
                        if (cp.hasBackgroundImage) {
                            ctx.drawImage(cp.htmlImage, 0, 0, canvas.width, canvas.height);
                        }

                        //forEach Layer {
                        //     paint ProductColorImage
                        //}
                        for (i = 0; i < pcs.length; i++) {
                            ctx.drawImage(pcs[i].htmlImage, 0, 0, canvas.width, canvas.height);
                        }
                        //Set ProductImage
                        cp.image = canvas.toDataURL("image/png");
                        //Save Product
                        cp.$save();
                    }
                }

                function rgbToHsl(r, g, b) {
                    r /= 255, g /= 255, b /= 255;
                    var max = Math.max(r, g, b),
                        min = Math.min(r, g, b);
                    var h, s, l = (max + min) / 2;

                    if (max == min) {
                        h = s = 0; // achromatic
                    } else {
                        var d = max - min;
                        s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
                        switch (max) {
                            case r:
                                h = (g - b) / d + (g < b ? 6 : 0);
                                break;
                            case g:
                                h = (b - r) / d + 2;
                                break;
                            case b:
                                h = (r - g) / d + 4;
                                break;
                        }
                        h /= 6;
                    }

                    return ({
                        h: h,
                        s: s,
                        l: l,
                    });
                }


                function hslToRgb(h, s, l) {
                    var r, g, b;

                    if (s == 0) {
                        r = g = b = l; // achromatic
                    } else {
                        function hue2rgb(p, q, t) {
                            if (t < 0) t += 1;
                            if (t > 1) t -= 1;
                            if (t < 1 / 6) return p + (q - p) * 6 * t;
                            if (t < 1 / 2) return q;
                            if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
                            return p;
                        }

                        var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                        var p = 2 * l - q;
                        r = hue2rgb(p, q, h + 1 / 3);
                        g = hue2rgb(p, q, h);
                        b = hue2rgb(p, q, h - 1 / 3);
                    }

                    return ({
                        r: Math.round(r * 255),
                        g: Math.round(g * 255),
                        b: Math.round(b * 255),
                    });
                }


                $scope.clearRecolor = function() {
                    $scope.drawImage();
                }

                $scope.$on('ProductLibraryChanged', function (event, args) {
                    loadProducts();
                });

                $scope.$on('clearWorkspace', function (event, args){
                    initReColor();
                });

                loadProducts();

            }
        };
    }]);
})();
