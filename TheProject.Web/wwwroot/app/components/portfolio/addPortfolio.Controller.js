(function () {
    'use strict';

    function AddPortfolioController($location, $scope, TheProjectService) {
        $scope.portfolio = {};
        $scope.selectedMunicipality = {};

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
        $scope.navigateTo = function (url) {
            $location.path(url);
        }

        $scope.savePortfolio = function (portfolio) {
            if (portfolio) {

                TheProjectService.addPortfolio(portfolio, function (data) {
                    if (data) {
                        $location.path('/viewPortfolios');
                    }
                });
            }
        }
    }

    angular.module('TheApp').controller('AddPortfolioController', AddPortfolioController);
    AddPortfolioController.$inject = ['$location', '$scope','TheProjectService'];
})();