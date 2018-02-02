(function () {
    'use strict';

    function ViewBuildingsController($location, $scope) {

        $scope.name = 'APP Name';
        $scope.navigateTo = function (url) {
            $location.path(url);
        }
    }

    angular.module('TheApp').controller('ViewBuildingsController', ViewBuildingsController);
    ViewBuildingsController.$inject = ['$location', '$scope'];
})();