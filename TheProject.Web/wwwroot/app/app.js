(function () {
    'use strict';

    var app = angular.module('TheApp', ['ngRoute', 'ngStorage']);

    app.constant('projectApi', 'http://localhost:63785/api');

    app.run(function ($rootScope, $location, $sessionStorage, $timeout) {
        $rootScope.$on('$routeChangeSuccess', function () {
            $rootScope.currentUrl = $location.path();
        });
    });
})();