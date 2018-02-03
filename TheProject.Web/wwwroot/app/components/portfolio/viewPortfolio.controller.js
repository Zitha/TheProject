(function () {
    'use strict';

    function ViewPortfoliosController($location, $scope, TheProjectService) {

        init();

        function init() {
            TheProjectService.getPortfolios(function (data) {
                if (data) {
                    $scope.Portfolios = data;
                }
            });
        }

        $scope.navigateTo = function (url) {
            $location.path(url);
        }
    }

    angular.module('TheApp').controller('ViewPortfoliosController', ViewPortfoliosController);
    ViewPortfoliosController.$inject = ['$location', '$scope','TheProjectService'];
})();