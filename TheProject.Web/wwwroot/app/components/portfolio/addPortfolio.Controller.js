(function () {
    'use strict';

    function AddPortfolioController($location, $scope, TheProjectService) {
        $scope.portfolio = {
            client: {}
        };
        $scope.selectedMunicipality = {};
        $scope.selectedClient = {};
        $scope.provices = ['Eastern Cape', 'Free State', 'Gauteng', 'KwaZulu-Natal', 'Limpopo', 'Mpumalanga', 'North West', 'Northern Cape', 'Western Cape'];

        $scope.clients = [];
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
            TheProjectService.getClients(function (data) {
                if (data) {
                    $scope.clients = data;
                }
            });
        }

        $scope.navigateTo = function (url) {
            $location.path(url);
        }

        $scope.savePortfolio = function (portfolio) {
            if (portfolio && portfolio.client.ClientName != undefined) {

                TheProjectService.addPortfolio(portfolio, function (data) {
                    if (data) {
                        $location.path('/viewPortfolios');
                    }
                });
            }
        }

        $scope.selectedClientChange = function (client) {
            $scope.selectedClient = client;
        }
    }

    angular.module('TheApp').controller('AddPortfolioController', AddPortfolioController);
    AddPortfolioController.$inject = ['$location', '$scope', 'TheProjectService'];
})();