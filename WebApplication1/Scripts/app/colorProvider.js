
var Analyze_Colors = [];
(function () {
    var app = angular.module('colorProvider', ['techmerServices']);

    app.directive("colorProvider", ['ColorPalette', 'Workspaces', function (ColorPalette, Workspaces) {
        return {
            restrict: 'EA',
            templateUrl: "/partials/colorProvider.html",
            controller: ["$rootScope", "$scope", function ($rootScope, $scope) {
                $scope.templateUrl = "/partials/colorProviderPopover.html";

                $scope.SelectedColor = { ColorSpace: "1" };


                $scope.rgb = { red: 0, green: 0, blue: 0 };

                $scope.lab = { l: 0, a: 0, b: 0 };
                $scope.hsv = { Hue: 0, Saturation: 0, Value: 0 };
                $scope.cmyk = { cyan: 0, magenta: 0, yellow: 0, key: 0 };
                $scope.hsl = { Hue: 0, Saturation: 0, lightness: 0 };
                $scope.hsi = { Hue: 0, Saturation: 0, intensity: 0 };
                $scope.hex = '';
                function loadWorkspace() {
                    Workspaces.currentWorkspace().then(function (workspace) {
                        $scope.currentWorkspace = workspace;
                    });
                }

                function loadColors() {

                    ColorPalette.list().then(function (colors) {
                        $scope.rgb.red = colors[0].colorData[0];
                        $scope.rgb.green = colors[0].colorData[1];
                        $scope.rgb.blue = colors[0].colorData[2];

                        $scope.lab.l = colors[0].lab[0];
                        $scope.lab.a = colors[0].lab[1];
                        $scope.lab.b = colors[0].lab[2];

                        rgbToOthers($scope.rgb.red, $scope.rgb.green, $scope.rgb.blue);

                    })
                }

                $scope.colorChange = function () {
                   
                    //if ($scope.rgb != '') {
                    //    rgbToOthers($scope.rgb.red, $scope.rgb.green, $scope.rgb.blue);
                    //}
                    //else if ($scope.lab != '') {
                    //    labToOthers($scope.lab.l, $scope.lab.a, $scope.lab.b);
                    //}
                    //else if ($scope.hsv != '') {
                    //    hsvToOthers($scope.hsv.Hue, $scope.hsv.Saturation, $scope.hsv.Value);
                    //}
                    //else if ($scope.cmyk != '') {
                    //    cmykToOthers($scope.cmyk.cyan, $scope.cmyk.magenta, $scope.cmyk.yellow, $scope.cmyk.key);
                    //}
                    //else if ($scope.hex != '') {
                    //    hexToOthers($scope.hex);
                    //}
                    //else {
                    //    return false;
                    //}
                    //colorData.rbg
                    if ($scope.rgb.red != null && $scope.rgb.green != null && $scope.rgb.blue != null) {

                        ColorPalette.addColor([$scope.rgb.red, $scope.rgb.green, $scope.rgb.blue, 1],
                        $scope.currentWorkspace.id,
                        [$scope.lab.l, $scope.lab.a, $scope.lab.b],
                        [$scope.hsv.Hue, $scope.hsv.Saturation, $scope.hsv.Value],
                        [$scope.cmyk.cyan, $scope.cmyk.magenta, $scope.cmyk.yellow, $scope.cmyk.key], $scope.hex
                        ).then(function (result) {
                            $rootScope.$broadcast('ColorSelectionChanged');
                        });
                    }
                    else {
                        return false;
                    }


                }

                function rgbToOthers(r, g, b) {
                    Analyze_Colors = '';
                    $scope.hex = chroma(r, g, b).hex();
                    var lab = chroma(r, g, b).lab();
                    if (lab.length > 0) {
                        $scope.lab.l = parseFloat((lab[0]).toFixed(2));
                        $scope.lab.a = parseFloat((lab[1]).toFixed(2));
                        $scope.lab.b = parseFloat((lab[2]).toFixed(2));
                    }
                    var hsv = chroma(r, g, b).hsv();
                    if (hsv.length > 0) {
                        $scope.hsv.Hue = parseFloat((hsv[0]).toFixed(2));
                        $scope.hsv.Saturation = parseFloat((hsv[1]).toFixed(2));
                        $scope.hsv.Value = parseFloat((hsv[2]).toFixed(2));
                    }
                    var cmyk = chroma(r, g, b).cmyk();
                    if (cmyk.length > 0) {
                        $scope.cmyk.cyan = parseFloat((cmyk[0]).toFixed(2));
                        $scope.cmyk.magenta = parseFloat((cmyk[1]).toFixed(2));
                        $scope.cmyk.yellow = parseFloat((cmyk[2]).toFixed(2));
                        $scope.cmyk.key = parseFloat((cmyk[3]).toFixed(2));
                    }
                    var hsi = chroma(r, g, b).hsi();
                    if (hsi.length > 0) {
                        $scope.hsi.Hue = parseFloat((hsi[0]).toFixed(2));
                        $scope.hsi.Saturation = parseFloat((hsi[1]).toFixed(2));
                        $scope.hsi.intensity = parseFloat((hsi[2]).toFixed(2));
                    }
                    var hsl = chroma(r, g, b).hsl();
                    if (hsl.length > 0) {
                        $scope.hsl.Hue = parseFloat((hsl[0]).toFixed(2));
                        $scope.hsl.Saturation = parseFloat((hsl[1]).toFixed(2));
                        $scope.hsl.lightness = parseFloat((hsl[2]).toFixed(2));
                    }
                    Analyze_Colors = {
                        'RGB': [$scope.rgb.red, $scope.rgb.green, $scope.rgb.blue],
                        'HEX': $scope.hex,
                        'HSL': [($scope.hsl.Hue), ($scope.hsl.Saturation), ($scope.hsl.lightness)],
                        'HSV': [($scope.hsv.Hue), ($scope.hsv.Saturation), ($scope.hsv.Value)],
                        'HSI': [($scope.hsi.Hue), ($scope.hsi.Saturation), ($scope.hsi.intensity)],
                        'LAB': [($scope.lab.l), ($scope.lab.a), ($scope.lab.b)]

                        //'CMYK': [($scope.cmyk.cyan).toFixed(2), ($scope.cmyk.magenta).toFixed(2), ($scope.cmyk.yellow).toFixed(2), ($scope.cmyk.key).toFixed(2)],


                    };

                    $rootScope.colorAnalyzerScope.Analyze_Colors = Analyze_Colors;
                    $rootScope.colorAnalyzerScope.Rgb.Red = Analyze_Colors.RGB[0]
                    $rootScope.colorAnalyzerScope.Rgb.Green = Analyze_Colors.RGB[1]
                    $rootScope.colorAnalyzerScope.Rgb.Blue = Analyze_Colors.RGB[2]
                }



                $scope.rgbToOthers = function (rgb) {
                    rgbToOthers(rgb.red, rgb.green, rgb.blue);
                }

                $scope.hexToOthers = function (hex) {
                    if (hex.length > 6) {
                        $scope.hex = hex;
                        var lab = chroma(hex).lab();
                        if (lab.length > 0) {
                            $scope.lab.l = parseFloat((lab[0]).toFixed(2));
                            $scope.lab.a = parseFloat((lab[1]).toFixed(2));
                            $scope.lab.b = parseFloat((lab[2]).toFixed(2));
                        }
                        var hsv = chroma(hex).hsv();
                        if (hsv.length > 0) {
                            $scope.hsv.Hue = parseFloat((hsv[0]).toFixed(2));
                            $scope.hsv.Saturation = parseFloat((hsv[1]).toFixed(2));
                            $scope.hsv.Value = parseFloat((hsv[2]).toFixed(2));
                        }
                        var cmyk = chroma(hex).cmyk();
                        if (cmyk.length > 0) {
                            $scope.cmyk.cyan = parseFloat((cmyk[0]).toFixed(2));
                            $scope.cmyk.magenta = parseFloat((cmyk[1]).toFixed(2));
                            $scope.cmyk.yellow = parseFloat((cmyk[2]).toFixed(2));
                            $scope.cmyk.key = parseFloat((cmyk[3]).toFixed(2));
                        }
                        var rgb = chroma(hex).rgb();
                        if (rgb.length > 0) {
                            $scope.rgb.red = parseFloat((rgb[0]).toFixed(2));
                            $scope.rgb.green = parseFloat((rgb[1]).toFixed(2));
                            $scope.rgb.blue = parseFloat((rgb[2]).toFixed(2));
                        }
                        var hsi = chroma(hex).hsi();
                        if (hsi.length > 0) {
                            $scope.hsi.Hue = parseFloat((hsi[0]).toFixed(2));
                            $scope.hsi.Saturation = parseFloat((hsi[1]).toFixed(2));
                            $scope.hsi.intensity = parseFloat((hsi[2]).toFixed(2));
                        }
                        var hsl = chroma(hex).hsl();
                        if (hsl.length > 0) {
                            $scope.hsl.Hue = parseFloat((hsl[0]).toFixed(2));
                            $scope.hsl.Saturation = parseFloat((hsl[1]).toFixed(2));
                            $scope.hsl.lightness = parseFloat((hsl[2]).toFixed(2));
                        }
                        Analyze_Colors = {
                            'RGB': [$scope.rgb.red, $scope.rgb.green, $scope.rgb.blue],
                            'HEX': $scope.hex,
                            'HSL': [($scope.hsl.Hue), ($scope.hsl.Saturation), ($scope.hsl.lightness)],
                            'HSV': [($scope.hsv.Hue), ($scope.hsv.Saturation), ($scope.hsv.Value)],
                            'HSI': [($scope.hsi.Hue), ($scope.hsi.Saturation), ($scope.hsi.intensity)],
                            'LAB': [($scope.lab.l), ($scope.lab.a), ($scope.lab.b)]
                        };
                        $rootScope.colorAnalyzerScope.Analyze_Colors = Analyze_Colors;
                        $rootScope.colorAnalyzerScope.Rgb.Red = Analyze_Colors.RGB[0]
                        $rootScope.colorAnalyzerScope.Rgb.Green = Analyze_Colors.RGB[1]
                        $rootScope.colorAnalyzerScope.Rgb.Blue = Analyze_Colors.RGB[2]
                    }
                }

                $scope.labToOthers = function (lab) {

                    $scope.hex = chroma(lab.l, lab.a, lab.b).hex();
                    var hsv = chroma(lab.l, lab.a, lab.b).hsv();
                    if (hsv.length > 0) {
                        $scope.hsv.Hue = parseFloat((hsv[0]).toFixed(2));
                        $scope.hsv.Saturation = parseFloat((hsv[1]).toFixed(2));
                        $scope.hsv.Value = parseFloat((hsv[2]).toFixed(2));
                    }
                    var cmyk = chroma(lab.l, lab.a, lab.b).cmyk();
                    if (cmyk.length > 0) {
                        $scope.cmyk.cyan = parseFloat((cmyk[0]).toFixed(2));
                        $scope.cmyk.magenta = parseFloat((cmyk[1]).toFixed(2));
                        $scope.cmyk.yellow = parseFloat((cmyk[2]).toFixed(2));
                        $scope.cmyk.key = parseFloat((cmyk[3]).toFixed(2));
                    }
                    var rgb = chroma(lab.l, lab.a, lab.b).rgb();
                    if (rgb.length > 0) {
                        $scope.rgb.red = parseFloat((rgb[0]).toFixed(2));
                        $scope.rgb.green = parseFloat((rgb[1]).toFixed(2));
                        $scope.rgb.blue = parseFloat((rgb[2]).toFixed(2));
                    }
                    var hsi = chroma(lab.l, lab.a, lab.b).hsi();
                    if (hsi.length > 0) {
                        $scope.hsi.Hue = parseFloat((hsi[0]).toFixed(2));
                        $scope.hsi.Saturation = parseFloat((hsi[1]).toFixed(2));
                        $scope.hsi.intensity = parseFloat((hsi[2]).toFixed(2));
                    }
                    var hsl = chroma(lab.l, lab.a, lab.b).hsl();
                    if (hsl.length > 0) {
                        $scope.hsl.Hue = parseFloat((hsl[0]).toFixed(2));
                        $scope.hsl.Saturation = parseFloat((hsl[1]).toFixed(2));
                        $scope.hsl.lightness = parseFloat((hsl[2]).toFixed(2));
                    }
                    Analyze_Colors = {
                        'RGB': [$scope.rgb.red, $scope.rgb.green, $scope.rgb.blue],
                        'HEX': $scope.hex,
                        'HSL': [($scope.hsl.Hue), ($scope.hsl.Saturation), ($scope.hsl.lightness)],
                        'HSV': [($scope.hsv.Hue), ($scope.hsv.Saturation), ($scope.hsv.Value)],
                        'HSI': [($scope.hsi.Hue), ($scope.hsi.Saturation), ($scope.hsi.intensity)],
                        'LAB': [($scope.lab.l), ($scope.lab.a), ($scope.lab.b)]
                    };
                    $rootScope.colorAnalyzerScope.Analyze_Colors = Analyze_Colors;
                    $rootScope.colorAnalyzerScope.Rgb.Red = Analyze_Colors.RGB[0]
                    $rootScope.colorAnalyzerScope.Rgb.Green = Analyze_Colors.RGB[1]
                    $rootScope.colorAnalyzerScope.Rgb.Blue = Analyze_Colors.RGB[2]
                }

                $scope.hsvToOthers = function (hsv) {
                    $scope.hex = chroma(hsv.Hue, hsv.Saturation, hsv.v).hex();
                    var lab = chroma(hsv.h, hsv.s, hsv.Value).lab();
                    if (lab.length > 0) {
                        $scope.lab.l = parseFloat((lab[0]).toFixed(2));
                        $scope.lab.a = parseFloat((lab[1]).toFixed(2));
                        $scope.lab.b = parseFloat((lab[2]).toFixed(2));
                    }
                    var cmyk = chroma(hsv.h, hsv.s, hsv.Value).cmyk();
                    if (cmyk.length > 0) {
                        $scope.cmyk.cyan = parseFloat((cmyk[0]).toFixed(2));
                        $scope.cmyk.magenta = parseFloat((cmyk[1]).toFixed(2));
                        $scope.cmyk.yellow = parseFloat((cmyk[2]).toFixed(2));
                        $scope.cmyk.key = parseFloat((cmyk[3]).toFixed(2));
                    }
                    var rgb = chroma(hsv.h, hsv.s, hsv.Value).rgb();
                    if (rgb.length > 0) {
                        $scope.rgb.red = parseFloat((rgb[0]).toFixed(2));
                        $scope.rgb.green = parseFloat((rgb[1]).toFixed(2));
                        $scope.rgb.blue = parseFloat((rgb[2]).toFixed(2));
                    }
                    var hsi = chroma(hsv.h, hsv.s, hsv.Value).hsi();
                    if (hsi.length > 0) {
                        $scope.hsi.Hue = parseFloat((hsi[0]).toFixed(2));
                        $scope.hsi.Saturation = parseFloat((hsi[1]).toFixed(2));
                        $scope.hsi.intensity = parseFloat((hsi[2]).toFixed(2));
                    }
                    var hsl = chroma(hsv.h, hsv.s, hsv.Value).hsl();
                    if (hsl.length > 0) {
                        $scope.hsl.Hue = parseFloat((hsl[0]).toFixed(2));
                        $scope.hsl.Saturation = parseFloat((hsl[1]).toFixed(2));
                        $scope.hsl.lightness = parseFloat((hsl[2]).toFixed(2));
                    }
                    Analyze_Colors = {
                        'RGB': [$scope.rgb.red, $scope.rgb.green, $scope.rgb.blue],
                        'HEX': $scope.hex,
                        'HSL': [($scope.hsl.Hue), ($scope.hsl.Saturation), ($scope.hsl.lightness)],
                        'HSV': [($scope.hsv.Hue), ($scope.hsv.Saturation), ($scope.hsv.Value)],
                        'HSI': [($scope.hsi.Hue), ($scope.hsi.Saturation), ($scope.hsi.intensity)],
                        'LAB': [($scope.lab.l), ($scope.lab.a), ($scope.lab.b)]
                    };
                    $rootScope.colorAnalyzerScope.Analyze_Colors = Analyze_Colors;
                    $rootScope.colorAnalyzerScope.Rgb.Red = Analyze_Colors.RGB[0]
                    $rootScope.colorAnalyzerScope.Rgb.Green = Analyze_Colors.RGB[1]
                    $rootScope.colorAnalyzerScope.Rgb.Blue = Analyze_Colors.RGB[2]
                }

                $scope.cmykToOthers = function (cmyk) {
                    $scope.hex = chroma(cmyk.cyan, cmyk.magenta, cmyk.yellow, cmyk.key).hex();

                    var lab = chroma(cmyk.cyan, cmyk.magenta, cmyk.yellow, cmyk.key).lab();
                    if (lab.length > 0) {
                        $scope.lab.l = parseFloat((lab[0]).toFixed(2));
                        $scope.lab.a = parseFloat((lab[1]).toFixed(2));
                        $scope.lab.b = parseFloat((lab[2]).toFixed(2));
                    }
                    var hsv = chroma(cmyk.cyan, cmyk.magenta, cmyk.yellow, cmyk.key).hsv();
                    if (hsv.length > 0) {
                        $scope.hsv.Hue = parseFloat((hsv[0]).toFixed(2));
                        $scope.hsv.Saturation = parseFloat((hsv[1]).toFixed(2));
                        $scope.hsv.Value = parseFloat((hsv[2]).toFixed(2));
                    }
                    var rgb = chroma(cmyk.cyan, cmyk.magenta, cmyk.yellow, cmyk.key).rgb();
                    if (rgb.length > 0) {
                        $scope.rgb.red = parseFloat((rgb[0]).toFixed(2));
                        $scope.rgb.green = parseFloat((rgb[1]).toFixed(2));
                        $scope.rgb.blue = parseFloat((rgb[2]).toFixed(2));
                    }
                    var hsi = chroma(cmyk.cyan, cmyk.magenta, cmyk.yellow, cmyk.key).hsi();
                    if (hsi.length > 0) {
                        $scope.hsi.Hue = parseFloat((hsi[0]).toFixed(2));
                        $scope.hsi.Saturation = parseFloat((hsi[1]).toFixed(2));
                        $scope.hsi.intensity = parseFloat((hsi[2]).toFixed(2));
                    }
                    var hsl = chroma(cmyk.cyan, cmyk.magenta, cmyk.yellow, cmyk.key).hsl();
                    if (hsl.length > 0) {
                        $scope.hsl.Hue = parseFloat((hsl[0]).toFixed(2));
                        $scope.hsl.Saturation = parseFloat((hsl[1]).toFixed(2));
                        $scope.hsl.lightness = parseFloat((hsl[2]).toFixed(2));
                    }
                    Analyze_Colors = {
                        'RGB': [$scope.rgb.red, $scope.rgb.green, $scope.rgb.blue],
                        'HEX': $scope.hex,
                        'HSL': [($scope.hsl.Hue), ($scope.hsl.Saturation), ($scope.hsl.lightness)],
                        'HSV': [($scope.hsv.Hue), ($scope.hsv.Saturation), ($scope.hsv.Value)],
                        'HSI': [($scope.hsi.Hue), ($scope.hsi.Saturation), ($scope.hsi.intensity)],
                        'LAB': [($scope.lab.l), ($scope.lab.a), ($scope.lab.b)]
                    };
                    $rootScope.colorAnalyzerScope.Analyze_Colors = Analyze_Colors;
                    $rootScope.colorAnalyzerScope.Rgb.Red = Analyze_Colors.RGB[0]
                    $rootScope.colorAnalyzerScope.Rgb.Green = Analyze_Colors.RGB[1]
                    $rootScope.colorAnalyzerScope.Rgb.Blue = Analyze_Colors.RGB[2]
                }

                $scope.$on('ColorSelectionChanged', function (event, args) {
                    loadColors();
                })

                loadWorkspace();
                loadColors();

            }]
        }

    }]); //End Directive (colorProvider)
})(); //End File