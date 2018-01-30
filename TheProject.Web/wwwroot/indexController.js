(function () {
    'use strict';

    function indexController($location, $scope) {

        $scope.name = 'APP Name';

    }

    angular.module('TheApp').controller('indexController', indexController);
    indexController.$inject = ['$location', '$scope'];
})();