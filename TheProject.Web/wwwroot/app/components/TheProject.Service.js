(function () {
    'use strict';

    function TheProjectService($http, $q, $sessionStorage, TheProjectFactory) {
        var self = this;
        self.getPortfolios = function (callback) {
            TheProjectFactory.getPortfolios().then(function (response) {
                callback(response);
            }, function (error) {
                //alertDialogService.setHeaderAndMessage('Error Has Occurred ', 'Unable to get contracts. Please retry again, if the issue persists contact administrator.');
                //var templateUrl = '/app/common/alert/infoDialog.template.html';
                //modal.show(templateUrl, 'alertDialogController');
            });
        }

        self.addPortfolio = function (portfolio, callback) {
            TheProjectFactory.addPortfolio(portfolio).then(function (response) {
                callback(response);
            }, function (error) {
                //alertDialogService.setHeaderAndMessage('Error Has Occurred ', 'Unable to get contracts. Please retry again, if the issue persists contact administrator.');
                //var templateUrl = '/app/common/alert/infoDialog.template.html';
                //modal.show(templateUrl, 'alertDialogController');
            });
        }

        self.addFacility = function (facility, callback) {
            TheProjectFactory.addFacility(facility).then(function (response) {
                callback(response);
            }, function (error) {
                //alertDialogService.setHeaderAndMessage('Error Has Occurred ', 'Unable to get contracts. Please retry again, if the issue persists contact administrator.');
                //var templateUrl = '/app/common/alert/infoDialog.template.html';
                //modal.show(templateUrl, 'alertDialogController');
            });
        }

        self.getFacilities = function (callback) {
            TheProjectFactory.getFacilities().then(function (response) {
                callback(response);
            }, function (error) {
                //alertDialogService.setHeaderAndMessage('Error Has Occurred ', 'Unable to get contracts. Please retry again, if the issue persists contact administrator.');
                //var templateUrl = '/app/common/alert/infoDialog.template.html';
                //modal.show(templateUrl, 'alertDialogController');
            });
        }
    }

    angular.module('TheApp').service('TheProjectService', TheProjectService);
    TheProjectService.$inject = ['$http', '$q', '$sessionStorage', 'TheProjectFactory'];
})();
