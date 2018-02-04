(function () {
    'use strict';

    function ViewFacilitiesController($location, $scope, TheProjectService) {

        init();
        function init() {
            TheProjectService.getFacilities(function (data) {
                if (data) {
                    $scope.Facilities = data;
                }
            });
        }

        //$scope.navigateTo = function (url) {
        //    $location.path(url);
        //}
    }

    angular.module('TheApp').controller('ViewFacilitiesController', ViewFacilitiesController);
    ViewFacilitiesController.$inject = ['$location', '$scope','TheProjectService'];
})();