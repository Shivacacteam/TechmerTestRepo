     var app = angular.module('uploader', ["ui.bootstrap","techmerInspirationProviders"]);

    app.directive("uploader", function() {
        return {
            scope: {},
        restrict: 'E',
        templateUrl: "/partials/uploader.html",
        controller: ["$rootScope", "$scope", "$uibModal","Workspaces", function ($rootScope, $scope, $modal, Workspaces) {
            
            $scope.isOpen = true;
            function loadWorkspaces(){
                Workspaces.list().then(function (workspaces) {
                    $scope.Workspaces = workspaces;
                    $scope.currentWorkspace = workspaces[0];

                    function initUploader() {
                        $scope.currentWorkspace.image = "";
                    };

                    if ($scope.currentWorkspace.image === undefined) {
                        initUploader();
                    }

                })
            }

            /*
            if ($routeParams.workspaceId) {
                $scope.Workspace = Workspaces.get({ workspaceId: $routeParams.workspaceId });
            } else {
                Workspaces.query(function (data) {
                    $scope.Workspaces = data;
                    $scope.Workspace = $scope.Workspaces[0];
                });
            }
            */

            $scope.selectInspiration = function (event) {
                if ($scope.opening) {
                    return;
                }
                $scope.opening = true;
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: "/partials/inspirationSelector.html",
                    controller: "inspirationSelectorCtrl",
                    size: "lg"
                });

                modalInstance.result.then(function (imageData) {
                    $scope.opening = false;
                    $scope.currentWorkspace.image = imageData;
                    $scope.currentWorkspace.$save();
                    $rootScope.$broadcast('InspirationChanged');
                }, function () {
                    $scope.opening = false;
                    //$log.info('Modal dismissed at: ' + new Date());
                })
            }


            $scope.clearWorkspace = function () {
                var modalInstance = $modal.open({
                    animation: true,
                    templateUrl: "/partials/clearWorkspaceModal.html",
                    controller: "modalClearWorkspaceCtrl",
                    size: "md"
                });

                modalInstance.result.then(function (clearWorkspace) {
                    if(clearWorkspace){
                        $scope.currentWorkspace = Workspaces.clearWorkspace($scope.currentWorkspace);
                        $rootScope.$broadcast('clearWorkspace', { workspace: $scope.currentWorkspace });
                    }
                }, function () {
                    //$log.info('Modal dismissed at: ' + new Date());
                });
                
            };

            
            $scope.processFile = function (file) {
               
                var fileReader = new FileReader();
                fileReader.onload = function(event){
                    var uri = event.target.result;
                    $scope.currentWorkspace.image = uri;
                    $scope.currentWorkspace.$save();
                    $scope.$apply();
                    $rootScope.$broadcast('InspirationChanged');

                };
                fileReader.readAsDataURL(file.file);
            };


            loadWorkspaces();

        }]
      };
    });

    app.controller("modalClearWorkspaceCtrl", ["$scope", "$uibModalInstance", function ($scope, $modalInstance) {

     $scope.ok = function () {
         $modalInstance.close(true);
     };

     $scope.cancel = function () {
         $modalInstance.dismiss('cancel');
     };
 }]);

    app.controller("inspirationSelectorCtrl", ["$scope", "$uibModalInstance", "$http", "$q", "Workspaces", function ($scope, $modalInstance, $http, $q, Workspaces) {

     $scope.selected = {
         image: "",
         provider: "",
         getImage: function(){}
     }
     $scope.data = {
         facebook: {},
         flickr: {},
         instagram: {},
         google: {}
     }

     Workspaces.list().then(function (workspaces) {
         $scope.selected = {
             image: workspaces[0].image,
             provider: "Workspace",
             getImage: function (image) {
                var deferred = $q.defer();
                deferred.resolve(image);
                return deferred.promise;
             }
         }

     })


     $scope.ok = function () {
         switch ($scope.selected.provider) {
             default:
                 $scope.selected.getImage($scope.selected.image).then(function (imageData) {
                     $modalInstance.close(imageData);
                 });
         }
     };

     $scope.cancel = function () {
         $modalInstance.dismiss('cancel');
     };
 }]);
