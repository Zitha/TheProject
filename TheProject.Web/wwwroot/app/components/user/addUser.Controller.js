(function () {
    'use strict';

    function AddUserController($location, $scope, TheProjectService) {
        $scope.user = {};

        $scope.roles = ['User', 'Admin'];
        $scope.navigateTo = function (url) {
            $location.path(url);
        }

        $scope.saveUser = function (user) {
            if (user) {
                TheProjectService.addUser(user, function (data) {
                    if (data) {

                    }
                });
            }
        }
    }

    angular.module('TheApp').controller('AddUserController', AddUserController);
    AddUserController.$inject = ['$location', '$scope', 'TheProjectService'];
})();