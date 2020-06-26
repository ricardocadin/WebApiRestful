(function (app) {
    'use strict';
    
    var baseApiUrl = 'http://localhost:65469/Api/';

    app.factory('apiService', apiService);

    apiService.$inject = ['$http', '$location', '$rootScope'];

    function apiService($http, $location, $rootScope) {
        var service = {
            get: get,
            post: post
        };
        
        function get(url, config, successCallback, failureCallback) {
            var apiUrl = createApiUrl(url),
                $promise = $http.get(apiUrl, config);

            return configurePromise($promise, successCallback, failureCallback);
        };

        function post(url, data, successCallback, failureCallback) {
            var apiUrl = createApiUrl(url),
                $promise = $http.post(apiUrl, data);
            
            return configurePromise($promise, successCallback, failureCallback);
        };
        
        function createApiUrl(url) {
            return baseApiUrl + url;
        };
        
        function configurePromise ($promise, successCallback, failureCallback) {
            return $promise
                    .then(function (result) {
                        if (isValidCallbackFunciton(successCallback))
                            successCallback(result);
                    }, function (error) {
                        if (isValidCallbackFunciton(failureCallback))
                            failureCallback(error);
                    });
        };
        
        function isValidCallbackFunciton(callbackFn) {
            return angular.isDefined(callbackFn) && angular.isFunction(callbackFn);
        };
        
        return service;
    };
})(angular.module('tcc-app'));