(function () {
    'use strict';

    function ViewBuildingsController($location, $scope, TheProjectService) {

        init();

        function init() {
            TheProjectService.getBuildings(function (data) {
                if (data) {
                    $scope.buildings = data;
                }
            });
        }

        $scope.navigateTo = function (url) {
            $location.path(url);
        }
    }

    angular.module('TheApp').controller('ViewBuildingsController', ViewBuildingsController);
    ViewBuildingsController.$inject = ['$location', '$scope','TheProjectService'];
})();