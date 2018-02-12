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
        
        $scope.viewFacility = function (facility) {
            TheProjectService.setSelectedFacility(facility);
            $location.path('/addFacility');
        }
    }

    angular.module('TheApp').controller('ViewFacilitiesController', ViewFacilitiesController);
    ViewFacilitiesController.$inject = ['$location', '$scope','TheProjectService'];
})();