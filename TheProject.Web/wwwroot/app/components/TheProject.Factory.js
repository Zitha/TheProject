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
        
        var getUnassignedFacilities = function (facility) {
            var defered = $q.defer();
            var getUnassignedFacilitiesComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.get(projectApi + '/Facility/GetUnassignedFacilities')
                .then(getUnassignedFacilitiesComplete, function (err, status) {
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

        //-----------------Building------------------------------
        var addBuilding = function (building) {
            var defered = $q.defer();
            var addBuildingComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.post(projectApi + '/Building/AddBuilding', building)
                .then(addBuildingComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        var updateBuilding = function (building) {
            var defered = $q.defer();
            var updateBuildingComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.post(projectApi + '/Building/UpdateBuilding', building)
                .then(updateBuildingComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        var getBuildingByFacilityId = function (facilityId) {
            var defered = $q.defer();
            var getBuildingByFacilityIdComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.get(projectApi + '/Building/GetBuildingByFacilityId?facilityId='+facilityId)
                .then(getBuildingByFacilityIdComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        var getBuildings = function () {
            var defered = $q.defer();
            var getBuildingsComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.get(projectApi + '/Building/GetBuildings')
                .then(getBuildingsComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        //-----------------Client------------------------------
        var getClients = function () {
            var defered = $q.defer();
            var getClientsComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.get(projectApi + '/Client/GetClients')
                .then(getClientsComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }

        var addClient = function (client) {
            var defered = $q.defer();
            var addClientComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.post(projectApi + '/Client/AddClient', client)
                .then(addClientComplete, function (err, status) {
                    defered.reject(err);
                });

            return defered.promise;
        }


        var getPotfoliosbyClientId = function () {
            var defered = $q.defer();
            var getClientsComplete = function (response) {
                defered.resolve(response.data);
            }

            $http.get(projectApi + '/Client/GetPotfoliosbyClientId')
                .then(getClientsComplete, function (err, status) {
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
            addClient: addClient,
            getUsers: getUsers,
            addBuilding: addBuilding,
            getClients: getClients,
            getPotfoliosbyClientId: getPotfoliosbyClientId,
            getBuildings: getBuildings,
            updateBuilding: updateBuilding,
            getUnassignedFacilities: getUnassignedFacilities
        };

    }

    angular.module('TheApp').service('TheProjectFactory', TheProjectFactory);
    TheProjectFactory.$inject = ['$http', '$q', 'projectApi'];
})();
