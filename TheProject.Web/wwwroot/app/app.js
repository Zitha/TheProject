(function () {
    'use strict';

    var app = angular.module('TheApp', ['ngRoute', 'ngStorage']);

    app.run(function ($rootScope, $location, $sessionStorage, $timeout) {
        $rootScope.$on('$routeChangeSuccess', function () {
            $rootScope.currentUrl = $location.path();
        });
    });
})();