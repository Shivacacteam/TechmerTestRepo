var app = angular.module('gridCompare', ['techmerServices']);

app.directive('gCompare', function () {
    return {
        restrict: 'E',
        templateUrl: "/partials/gridCompare.html",
        controller: ["$scope", "$timeout", "$interval", "AssetLayerSystem", "Asset", function ($scope, $timeout, $interval, AssetLayerSystem, Asset) {
            $scope.AssetSaveNeeded = false;
            var stopSave; //Needed to cancel the inteval when the user leaves the compare page.

            var saveAssets = function () {
                if ($scope.AssetSaveNeeded) {
                    Asset.save($scope.visibleAssets)
                        .$promise.then(function (results) {
                            $scope.AssetSaveNeeded = false;
                        });
                }
            }

            function loadData(){
                AssetLayerSystem.visibleAssets().then(function (result) {
                    $scope.visibleAssets = result;
                    $scope.$applyAsync();
                });
            }

            interact('.gridCompareDraggable')
                .draggable({
                    inertia: false,
                    restrict: {
                        restriction: '.gridCompareContainer',
                        endonly: true
                    },
                    autoScroll: false,
                    onstart: onStartListener,
                    onmove: onMoveListener,
                    onend: onEndListener
                })
                .gesturable({
                    onstart:onStartListener,
                    onmove: onMoveListener,
                    onend: onEndListener
                });

            function onMoveListener(event) {
                var asset = angular.element(event.target).scope().asset,
                    target = event.target,
                    x = (asset.x || 0) + event.dx,
                    y = (asset.y || 0) + event.dy,
                    r = (asset.r || 0),
                    s = (asset.s || 1);
                    
                
                if (!isNaN(event.da))
                {
                    r += event.da;
                }
                if (!isNaN(event.ds)) {
                    s = s * (1 + event.ds);
                    if (s < .25) {
                        s = .25;
                    }
                }
                asset.style = {
                    zIndex: asset.zIndex,
                    position: 'absolute',
                    left:0,
                    top: 0,
                    float:"left",
                    transform: 'translate(' + x + 'px, ' + y + 'px) rotate(' + r + 'deg) scale(' + s + ')',
                    webkitTransform: 'translate(' + x + 'px, ' + y + 'px) rotate(' + r + 'deg) scale(' + s + ')',
                    MsTouchAction: 'none',
                    touchAction: 'none'
                }

                
                target.style.webkitTransform = target.style.transform = 'translate(' + x + 'px, ' + y + 'px) rotate(' + r + 'deg) scale(' + s + ')';
                
                asset.x = x;
                asset.y = y;
                asset.r = r;
                asset.s = s;
            }

            function onStartListener(event) {
                var asset = angular.element(event.target).scope().asset;
                asset.zIndex = 99;
                AssetLayerSystem.addAsset(asset).then(function (result) {
                    $scope.visibleAssets = result;
                    $scope.$applyAsync();
                });
            }

            function onEndListener(event) {
                $scope.AssetSaveNeeded = true;
                event.preventDefault(event);
                event.stopPropagation();
                return false;
            }

            $scope.assetClicked = function ($event, asset) {
                AssetLayerSystem.addAsset(asset).then(function (result) {
                    $scope.visibleAssets = result;
                    $scope.$applyAsync();
                });
            }

            $timeout(function () {
                stopSave = $interval(saveAssets, 30 * 1000);
            })

            $scope.$on('clearWorkspace', function (event, args) {
                loadData();
            })

            $scope.$on('$destroy', function () {
                $interval.cancel(stopSave);
                if ($scope.AssetSaveNeeded){
                    Asset.save($scope.visibleAssets);
                    $scope.AssetSaveNeeded = false;
                }
                
            });

            $scope.$on('compareVisibilityChange', function (event, args) {
                AssetLayerSystem.visibleAssets().then(function (result) {
                    $scope.visibleAssets = result;
                    $scope.$applyAsync();
                });
            })

            loadData();

        }]
    }
})