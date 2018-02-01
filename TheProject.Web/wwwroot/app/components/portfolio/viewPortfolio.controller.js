(function () {
    'use strict';

    function ViewPortfolioController($location, $scope) {

        $scope.name = 'APP Name';
        $scope.navigateTo = function (url) {
            $location.path(url);
        }
    }

    angular.module('TheApp').controller('ViewPortfolioController', ViewPortfolioController);
    ViewPortfolioController.$inject = ['$location', '$scope'];
})();