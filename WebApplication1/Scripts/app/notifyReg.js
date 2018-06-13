var app = angular.module('notifyReg', []);
app.controller('validateCtrl', [ "$scope", function ($scope) {
    $scope.fName = '';
    $scope.lName = '';
    $scope.cName = '';
    $scope.eMail = '';
}]);