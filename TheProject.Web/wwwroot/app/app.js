(function () {
    'use strict';

    var app = angular.module('TheApp', ['ngRoute', 'ngStorage']);
    //154.0.170.81:89
    app.constant('projectApi', 'http://localhost:63785/api');
    //app.constant('projectApi', 'http://154.0.170.81:89/api');

    app.run(function ($rootScope, $location, $sessionStorage, $timeout) {
        $rootScope.$on('$routeChangeSuccess', function () {
            $rootScope.currentUrl = $location.path();
        });
    });
})();