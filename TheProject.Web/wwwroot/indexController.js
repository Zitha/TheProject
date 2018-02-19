(function () {
    'use strict';

    function indexController($location, $scope, $rootScope,$sessionStorage) {

        $scope.isLoggedin = $sessionStorage.isUserAuthenticated;
        $scope.navigateTo = function (url) {
            $location.path(url);
        }


        $rootScope.$on('$locationChangeSuccess', routeChanged);
        function routeChanged(evt, newUrl, oldUrl) {
            if ($location.path() == '/index' || $location.path() == '/') {
                $location.path('/index');
            }
        }
    }

    angular.module('TheApp').controller('indexController', indexController);
    indexController.$inject = ['$location', '$scope', '$rootScope','$sessionStorage'];
})();