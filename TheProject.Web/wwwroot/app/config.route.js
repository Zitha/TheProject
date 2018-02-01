(function () {
    'use strict';
    var routeProvider = function ($routeProvider, $locationProvider) {

        var viewBase = '../app/components/';
        $routeProvider.when('/addFacility', {
            controller: 'AddFacilityController',
            templateUrl: viewBase + '/facility/add.facility.html',
        }).when('/viewFacilities', {
            controller: 'ViewFacilitiesController',
            templateUrl: viewBase + '/facility/view.facilities.html',
        }).otherwise({ redirectTo: '/' });
    }
    angular.module('TheApp').config(['$routeProvider', '$locationProvider', routeProvider]);
    routeProvider.$inject = ['$routeProvider', '$locationProvider'];

})();