(function () {
    'use strict';

    function AddClientController($location, $scope, TheProjectService) {

        $scope.name = 'APP Name';
        $scope.navigateTo = function (url) {
            $location.path(url);
        }

        $scope.saveClient = function (client) {
            if (client) {
                TheProjectService.addClient(client, function (data) {
                    if (data) {
                        $location.path('/viewClients');
                    }
                });
            }
        }
    }

    angular.module('TheApp').controller('AddClientController', AddClientController);
    AddClientController.$inject = ['$location', '$scope','TheProjectService'];
})();