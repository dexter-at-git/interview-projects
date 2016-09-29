(function () {
    'use strict';

    angular
        .module('beerManagerApp')
        .factory('beerManagerService', beerManagerService);

    beerManagerService.$inject = ['$http', '$httpParamSerializer'];

    function beerManagerService($http, $httpParamSerializer) {

        var baseUrl = "../api/beermanager/beers";

        var service = {
            getBeers: getBeers,
        };

        function getBeers(params) {
            return $http({
                method: 'GET',
                url: baseUrl + '?' + $httpParamSerializer(params)
            });
        }

        return service;
    }
})();