(function () {
    'use strict';

    function ViewUsersController($location, $scope, TheProjectService) {

        init();

        function init() {
            TheProjectService.getUsers(function (data) {
                if (data) {
                    $scope.Users = data;
                }
            });
        }

        $scope.navigateTo = function (url) {
            $location.path(url);
        }
    }

    angular.module('TheApp').controller('ViewUsersController', ViewUsersController);
    ViewUsersController.$inject = ['$location', '$scope','TheProjectService'];
})();