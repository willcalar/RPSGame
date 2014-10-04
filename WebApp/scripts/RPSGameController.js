'use strict';

var RPSGameController = angular.module('RPSGameController',[]);
var url = 'http://localhost:6159/DataService.svc';

RPSGameController.factory('dataSourceFactory', function ($http) {
    return {
        getTopPlayers: function () {
            return $http.get(url + '/api/championship/top?count=10');
        },
        deleteAllData: function () {
            return $http.get(url + '/api/championship/delete');
        },
        newChampionship: function (data) {
            return $http.post(url + '/api/championship/new', data);
        }
    };
});

RPSGameController.controller('RPSGameController', function ($scope, $modal, $log, dataSourceFactory) {
    $scope.showContent = function ($fileContent) {
        dataSourceFactory.newChampionship($fileContent).success(newChampionshipSuccessCallback).error(errorCallback);
    };

    var deleteAllDataSuccessCallback = function (data, status) {
        dataSourceFactory.getTopPlayers().success(getTopPlayersSuccessCallback).error(errorCallback);
    };

    var getTopPlayersSuccessCallback = function (data, status) {
        $scope.champsList = data;
    };

    var newChampionshipSuccessCallback = function (data, status) {
        
        $scope.content = "Player: " + data[0].User + " with" + data[0].PlayCall;
    };

    $scope.deleteAllRecords = function () {
        dataSourceFactory.deleteAllData().success(deleteAllDataSuccessCallback).error(errorCallback);
    };

    var errorCallback = function (data, status, headers, config) {
        //notificationFactory.error(data.ExceptionMessage);
        $log.error(data.ExceptionMessage);
    };

    dataSourceFactory.getTopPlayers().success(getTopPlayersSuccessCallback).error(errorCallback);
});

RPSGameController.directive('onReadFile', function ($parse) {
    return {
        restrict: 'A',
        scope: false,
        link: function (scope, element, attrs) {
            var fn = $parse(attrs.onReadFile);

            element.on('change', function (onChangeEvent) {
                var reader = new FileReader();

                reader.onload = function (onLoadEvent) {
                    scope.$apply(function () {
                        fn(scope, { $fileContent: onLoadEvent.target.result });
                    });
                };

                reader.readAsText((onChangeEvent.srcElement || onChangeEvent.target).files[0]);
            });
        }
    };
});
