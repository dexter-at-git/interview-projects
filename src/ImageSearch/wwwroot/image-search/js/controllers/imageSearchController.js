(function () {
    'use strict';

    angular
        .module('imageSearchApp')
        .controller('imageSearchController', imageSearchController);

    imageSearchController.$inject = ['$filter', 'imageSearchService', 'localStorageService'];

    function imageSearchController($filter, imageSearchService, localStorageService) {

        var vm = this;
        vm.searchQuery = '';
        vm.searchResults = [];
        vm.searchHistory = [];
        vm.error = undefined;

        initialize();

        function initialize() {
            vm.searchHistory = localStorageService.getSavedResults();
        }

        vm.saveSearchResults = function () {
            localStorageService.saveResults(vm.searchResults);
            vm.searchHistory = localStorageService.getSavedResults();
        }

        vm.removeElement = function (index) {
            vm.searchResults.splice(index, 1);
        }

        vm.restoreHistory = function (key) {
            var searchRecord = $filter('filter')(vm.searchHistory, { key: key })[0];
            if (!searchRecord) {
                return;
            }
            vm.searchResults = searchRecord.searchResults;
        }

        vm.search = function () {
            if (vm.searchQuery === '') {
                return;
            }
            vm.searchImagePromise = imageSearchService.searchImages(vm.searchQuery)
                .then(function (result) {
                    vm.error = undefined;
                    vm.searchResults = result;
                }, function (error) {
                    vm.error = error.data;
                });
        }
    }

})();