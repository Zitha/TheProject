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

        $scope.viewBuiding = function (building) {
            TheProjectService.setSelectedBuilding(building);
            $location.path('/addBuilding');
        }
    }

    angular.module('TheApp').controller('ViewBuildingsController', ViewBuildingsController);
    ViewBuildingsController.$inject = ['$location', '$scope','TheProjectService'];
})();