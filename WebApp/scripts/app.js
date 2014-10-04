/// <reference path="../" />
'use strict';


var RPSGameApp = angular.module('RPSGame', [
    'RPSGameController',
    'ngRoute',
    'ui.bootstrap'
]);

RPSGameApp.config(['$routeProvider', '$controllerProvider',
  function ($routeProvider, $controllerProvider) {    

      $routeProvider.
        when('/index', {
            templateUrl: 'partials/Home.html'
        }).
        otherwise({
            redirectTo: '/index'
        });
  }]);

