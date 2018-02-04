(function () {
    'use strict';

    function TheProjectFactory($http, $q, projectApi) {

        var getPortfolios = function () {
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


        //------------------Facility-----------------
        var addFacility = function (facility) {
            var defered = $q.defer();
            var addFacilityComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.post(projectApi + '/Facility/AddFacility', facility)
                .then(addFacilityComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }


        var getFacilities = function () {
            var defered = $q.defer();
            var getFacilitiesComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.get(projectApi + '/Facility/GetFacilities')
                .then(getFacilitiesComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        //------------------User-----------------
        var addUser = function (user) {
            var defered = $q.defer();
            var addUserComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.post(projectApi + '/User/AddUser', user)
                .then(addUserComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        var getUsers = function () {
            var defered = $q.defer();
            var getUsersComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.get(projectApi + '/User/GetUsers')
                .then(getUsersComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        return {
            getPortfolios: getPortfolios,
            addPortfolio: addPortfolio,
            addFacility: addFacility,
            getFacilities: getFacilities,
            addUser: addUser,
            getUsers: getUsers
        };

    }

    angular.module('TheApp').service('TheProjectFactory', TheProjectFactory);
    TheProjectFactory.$inject = ['$http', '$q', 'projectApi'];
})();
