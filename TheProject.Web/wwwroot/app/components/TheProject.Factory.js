(function () {
    'use strict';

    function TheProjectFactory($http, $q, projectApi) {

        var getPortfolios = function (itemType) {
            var defered = $q.defer();
            var getPortfoliosComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.get(projectApi + '/Portfolio/GetPortfolios')
                .then(getPortfoliosComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }


        var addPortfolio = function (portfolio) {
            var defered = $q.defer();
            var addPortfolioComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.post(projectApi + '/Portfolio/AddPortfolio', portfolio)
                .then(addPortfolioComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        return {
            getPortfolios: getPortfolios,
            addPortfolio: addPortfolio
        };

    }

    angular.module('TheApp').service('TheProjectFactory', TheProjectFactory);
    TheProjectFactory.$inject = ['$http', '$q', 'projectApi'];
})();
