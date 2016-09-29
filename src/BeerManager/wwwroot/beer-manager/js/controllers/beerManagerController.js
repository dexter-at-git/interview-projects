(function () {
    'use strict';

    angular
        .module('beerManagerApp')
        .controller('beerManagerController', beerManagerController);

    beerManagerController.$inject = ['beerManagerService'];

    function beerManagerController(beerManagerService) {

        var vm = this;
        vm.beerList = [];
        vm.pager = {};
        vm.sorter = {};
        vm.filter = {};
        vm.error = {};

        function initialize() {
            vm.pager = new Pager();
            vm.sorter = new Sorter();
            vm.filter = new Filter();
            query();
        }

        function query() {
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

        var Sorter = function () {
            var self = this;
            self.order = 'name';
            self.reverse = false;

            self.sort = function (columnName) {
                self.order = columnName;
                self.reverse = !self.reverse;
                query();
            }
        }

        var Filter = function () {
            var self = this;
            self.name = '';

            self.filter = function () {
                query();
            }

            self.isDisabled = function () {
                return self.name !== '';
            }
        }

        var Pager = function () {
            var self = this;
            self.current = 1;
            self.total = 1;

            self.next = function () {
                self.current++;
                query();
            };

            self.prev = function () {
                self.current--;
                query();
            };

            self.hasPrev = function () {
                return self.current !== 1;
            }

            self.hasNext = function () {
                return self.current !== self.total;
            }
        };

        initialize();

    }
})();