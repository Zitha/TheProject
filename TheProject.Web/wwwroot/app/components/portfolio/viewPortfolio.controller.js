(function () {
    'use strict';

    function ViewPortfoliosController($location, $scope) {

        $scope.name = 'APP Name';
        $scope.navigateTo = function (url) {
            $location.path(url);
        }
    }

    angular.module('TheApp').controller('ViewPortfoliosController', ViewPortfoliosController);
    ViewPortfoliosController.$inject = ['$location', '$scope'];
})();