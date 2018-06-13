var app = angular.module('techmerGallerySelector', ["ui.bootstrap"]);

app.directive("gallerySelector", function () {
    return {
        restrict: 'E',
        templateUrl: "/Directives/gallerySelector/gallerySelector.html",
        scope: {
            items: "=",
            selectMethod: "&"
        },
        controller: ["$scope", "$q", function ($scope, $q) {

            $scope.itemsPerPage = 12;
            $scope.currentPage = 0;

            $scope.selectItem = function (item) {
                $scope.selectMethod()(item);
            }

            $scope.range = function () {
                var rangeSize = Math.max(2, Math.ceil($scope.items.length / $scope.itemsPerPage));
                var ret = [];
                var start;

                start = $scope.currentPage;
                if (start > $scope.pageCount() - rangeSize) {
                    start = $scope.pageCount() - rangeSize + 1;
                }

                for (var i = start; i < start + rangeSize; i++) {
                    ret.push(i);
                }
                return ret;
            };

            $scope.prevPage = function () {
                if ($scope.currentPage > 0) {
                    $scope.currentPage--;
                }
            };

            $scope.prevPageDisabled = function () {
                return $scope.currentPage === 0 ? "disabled" : "";
            };

            $scope.pageCount = function () {
                return Math.ceil($scope.items.length / $scope.itemsPerPage) - 1;
            };

            $scope.nextPage = function () {
                if ($scope.currentPage < $scope.pageCount()) {
                    $scope.currentPage++;
                }
            };

            $scope.nextPageDisabled = function () {
                return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
            };

            $scope.setPage = function (pageNum) {
                $scope.currentPage = pageNum;
            }

        }]
    };
});