(function () {
    'use strict';

    function AddUserController($location, $scope, TheProjectService) {
        $scope.hidePassword = false;
        $scope.user = TheProjectService.getSelectedUser();
        init();
        function init() {
            if ($scope.user.Id != undefined) {
                $scope.hidePassword = true;
                TheProjectService.getUnassignedFacilities(function (data) {

                    if (data) {
                        $scope.facilities = data;
                    }
                });
            }
        }

        $scope.roles = ['User', 'Admin'];
        $scope.navigateTo = function (url) {
            $location.path(url);
        }

        $scope.saveUser = function (user) {
            if (user) {
                TheProjectService.addUser(user, function (data) {
                    if (data) {
                        $location.path('/viewUsers');
                    }
                });
            }
        }
    }

    angular.module('TheApp').controller('AddUserController', AddUserController);
    AddUserController.$inject = ['$location', '$scope', 'TheProjectService'];
})();