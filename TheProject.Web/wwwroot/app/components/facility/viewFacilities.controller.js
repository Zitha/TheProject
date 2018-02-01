(function () {
    'use strict';

    function ViewFacilitiesController($location, $scope) {

        $scope.name = 'APP Name';
        $scope.navigateTo = function (url) {
            $location.path(url);
        }
    }

    angular.module('TheApp').controller('ViewFacilitiesController', ViewFacilitiesController);
    ViewFacilitiesController.$inject = ['$location', '$scope'];
})();