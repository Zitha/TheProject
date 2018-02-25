(function () {
    'use strict';

    function AddBuildingController($location, $scope, TheProjectService) {

        var isEdit = false;
        $scope.building = TheProjectService.getSelectedBuilding();

        if ($scope.building != undefined && $scope.building.BuildingNumber) {
            isEdit = true;
        } else {
            $scope.building = {
                buildingNumber: Math.random()
            };
        }

        $scope.selectedMunicipality = {};

        $scope.provices = ['Eastern Cape', 'Free State', 'Gauteng', 'KwaZulu-Natal', 'Limpopo', 'Mpumalanga', 'North West', 'Northern Cape', 'Western Cape'];

        $scope.facilities = [];

        $scope.buildingStatuses = ['Vacant', 'Occupied'];

        $scope.buildingTypes = ['Community Hall', 'Carport', 'Courthouse', 'City Hall', 'Embassy', 'School Administration Block',
            'Biology Lab', 'Computer Lab', 'Double Storey Classroom Block', 'Classroom Block'];

        $scope.buildingStandards = [
            { purpose: 'Highly sensitive purpose with critical results (e.g. hospital operating theatre) or high profile public building (e.g. Parliament House).  ', rating: 'S1' },
            {
                purpose: 'Good public presentation and a high-quality working/living environment are necessary (e.g. CBD building, Multi unit dwellings, Retail units).', rating: 'S2'
            },
            { purpose: 'Functionally-focused 	building 	(e.g. laboratory, public carparks).  ', rating: 'S3' },
            { purpose: 'Ancillary functions only with no critical operational role (e.g. storage) or building has a limited life.  ', rating: 'S4' },
            { purpose: 'Building is no longer operational - it is dormant, pending disposal, demolition, etc.  ', rating: 'S5' }
        ];

        init();
        function init() {
            TheProjectService.getFacilities(function (data) {
                if (data) {
                    $scope.facilities = data;
                }
            });
        }

        $scope.navigateTo = function (url) {
            $location.path(url);
        }

        $scope.saveBuilding = function (building) {
            if (building && !isEdit) {
                TheProjectService.addBuilding(building, function (data) {
                    if (data) {
                        $location.path('/viewBuildings');
                    }
                });
            } else {
                if (isEdit) {
                    building.Id = $scope.building.Id;
                    TheProjectService.updateBuilding(building, function (data) {
                        if (data) {
                            $location.path('/viewBuildings');
                        }
                    });
                }
            }
        }
    }

    angular.module('TheApp').controller('AddBuildingController', AddBuildingController);
    AddBuildingController.$inject = ['$location', '$scope', 'TheProjectService'];
})();