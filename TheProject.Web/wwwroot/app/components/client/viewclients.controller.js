(function () {
    'use strict';

    function ViewClientsController($location, $scope, TheProjectService) {

        init();

        function init() {
            TheProjectService.getClients(function (data) {
                if (data) {
                    $scope.Clients = data;
                }
            });
        }

        $scope.navigateTo = function (url) {
            $location.path(url);
        }
    }

    angular.module('TheApp').controller('ViewClientsController', ViewClientsController);
    ViewClientsController.$inject = ['$location', '$scope','TheProjectService'];
})();