(function () {
    'use strict';

    angular
        .module('imageSearchApp')
        .factory('localStorageService', localStorageService);

    localStorageService.$inject = [];

    function localStorageService() {

        var service = {
            getSavedResults: getSavedResults,
            saveResults: saveResults
        };

        function getSavedResults() {
            var items = [];
            for (var index = 0, length = localStorage.length; index < length; ++index) {
                var key = localStorage.key(index);
                var item = {
                    key: key,
                    date: new Date(parseInt(key)),
                    searchResults: angular.fromJson(localStorage.getItem(key))
                };
                items.push(item);
            }
            return items;
        }

        function saveResults(data) {
            localStorage.setItem(new Date().getTime(), angular.toJson(data));
        }

        return service;
    }

})();