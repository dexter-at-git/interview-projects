(function () {
    'use strict';

    angular
        .module('beerManagerApp')
        .controller('beerManagerController', beerManagerController);

    beerManagerController.$inject = ['beerManagerService'];

    function beerManagerController(beerManagerService) {
        var vm = this;
        vm.beerList = [];
        vm.pager = new function () {
            var self = this;
            self.current = 1;
            self.total = 1;
            self.hasPrev = function () {
                return self.current !== 1;
            }
            self.hasNext = function () {
                return self.current !== self.total;
            }
        };
        vm.sorter = new function () {
            var self = this;
            self.order = 'name';
            self.reverse = false;
        };
        vm.filter = new function () {
            var self = this;
            self.name = '';
            self.isDisabled = function () {
                return self.name !== '';
            }
        };
        vm.error = {};

        function initialize() {
            getBeers();
        }

        function getBeers() {
            var params = {
                page: vm.pager.current,
                reverse: vm.sorter.reverse,
                order: vm.sorter.order,
                name: vm.filter.name
            };

            vm.getBeerPromise = beerManagerService.getBeers(params)
                .then(function (result) {
                    vm.beerList = result.data.data;
                    vm.pager.current = result.data.currentPage;
                    vm.pager.total = result.data.numberOfPages;
                }, function (error) {
                    vm.error = error.data;
                });
        }

        vm.sortData = function (columnName) {
            vm.sorter.order = columnName;
            vm.sorter.reverse = !vm.sorter.reverse;
            getBeers();
        }

        vm.filterData = function () {
            vm.pager.current = 1;
            getBeers();
        }

        vm.nextData = function () {
            vm.pager.current++;
            getBeers();
        };

        vm.prevData = function () {
            vm.pager.current--;
            getBeers();
        };

        initialize();

    }
})();