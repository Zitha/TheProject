(function () {
    'use strict';

    function AddFacilityController($location, $scope, TheProjectService) {
        $scope.facility = TheProjectService.getSelectedFacility();
        $scope.selectedMunicipality = {};
        $scope.portfolios = [];
        $scope.portfolio = {}
        $scope.provices = ['Eastern Cape', 'Free State', 'Gauteng', 'KwaZulu-Natal', 'Limpopo', 'Mpumalanga', 'North West', 'Northern Cape', 'Western Cape'];

        $scope.settlementTypes = ['Fire Station', 'Gate House', 'Industrial', 'Community Library'];

        $scope.municipalities = [{ name: 'City of Johannesburg Metropolitan Municipality', code: 'JHB' },
        { name: 'City of Tshwane Metropolitan Municipality', code: 'TSH' },
        { name: 'Ekurhuleni Metropolitan Municipality', code: 'EKU' },
        { name: 'Emfuleni Local Municipality', code: 'GT421' },
        { name: 'Lesedi Local Municipality', code: 'GT423' },
        { name: 'Merafong City Local Municipality', code: 'GT484' },
        { name: 'Midvaal Local Municipality', code: 'GT422' },
        { name: 'Mogale City Local Municipality', code: 'GT481' },
        { name: 'Rand West City Local Municipality	', code: 'GT485' }
        ];

        init();

        function init() {
            TheProjectService.getPortfolios(function (data) {
                if (data) {
                    $scope.portfolios = data;
                }
            });
        }

        $scope.navigateTo = function (url) {
            $location.path(url);
        }

        $scope.portfolioChanged = function () {
            $scope.facility.portfolio = $scope.portfolio;
            $scope.facility.client = {
                ClientCode: $scope.portfolio.Client.ClientId
            };
            //facility.portfolio
        }


        $scope.saveFacility = function (facility) {
            if (facility) {
                TheProjectService.addFacility(facility, function (data) {
                    if (data) {
                        $location.path('/viewFacilities');
                    }
                });
            }
        }
    }

    angular.module('TheApp').controller('AddFacilityController', AddFacilityController);
    AddFacilityController.$inject = ['$location', '$scope', 'TheProjectService'];
})();