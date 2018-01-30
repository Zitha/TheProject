(function () {
    'use strict';

    var app = angular.module('TheApp', ['ngRoute', 'toastr', 'ngStorage']);

    app.run(function ($rootScope, $location, $sessionStorage, $timeout) {
        $rootScope.$on('$routeChangeSuccess', function () {
            $rootScope.currentUrl = $location.path();
        });
    });
})();