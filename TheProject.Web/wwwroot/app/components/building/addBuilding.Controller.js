(function () {
    'use strict';

    function AddBuildingController($location, $scope) {
        $scope.building = {
            buildingNumber: Math.random()
        };
        $scope.selectedMunicipality = {};

        $scope.provices = ['Eastern Cape', 'Free State', 'Gauteng', 'KwaZulu-Natal', 'Limpopo', 'Mpumalanga', 'North West', 'Northern Cape', 'Western Cape'];

        $scope.buildingStatuses = ['Vacant', 'Occupied'];

        $scope.buildingTypes = ['Community Hall', 'Carport', 'Courthouse', 'City Hall', 'Embassy', 'School Administration Block',
            'Biology Lab', 'Computer Lab', 'Double Storey Classroom Block', 'Classroom Block'];

        $scope.buildingStandards = [
            { purpose: 'Highly sensitive purpose with critical results (e.g. hospital operating theatre) or high profile public building (e.g. Parliament House).  ', rating: 'S1' },
            {
                purpose: 'Good public presentation and a high-quality working/living environment are necessary (e.g. CBD building, Multi unit dwellings, Retail units).', rating: 'S2' },
            { purpose: 'Functionally-focused 	building 	(e.g. laboratory, public carparks).  ', rating: 'S3' },
            { purpose: 'Ancillary functions only with no critical operational role (e.g. storage) or building has a limited life.  ', rating: 'S4' },
            { purpose: 'Building is no longer operational - it is dormant, pending disposal, demolition, etc.  ', rating: 'S5' }
        ];
        $scope.navigateTo = function (url) {
            $location.path(url);
        }

        $scope.saveFacility = function (facility) {
            if (facility) {

            }
        }
    }

    angular.module('TheApp').controller('AddBuildingController', AddBuildingController);
    AddBuildingController.$inject = ['$location', '$scope'];
})();