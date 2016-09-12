(function () {
    'use strict';

    angular
        .module('smsManagerApp')
        .controller('smsManagerStatisticsController', smsManagerStatisticsController);

    smsManagerStatisticsController.$inject = ['smsManagerService'];

    function smsManagerStatisticsController(smsManagerService) {

        var vm = this;
        vm.error = undefined;
        vm.dateFrom = '2016-09-10';
        vm.dateTo = '2016-09-15';
        vm.mccList = '262,232,260';
        vm.statistics = [];

        vm.filter = function () {

            var statisticsRequest = {
                dateFrom: vm.dateFrom,
                dateTo: vm.dateTo,
                mccList: vm.mccList.split(',')
            }

            vm.statisticsPromise = smsManagerService.statistics(statisticsRequest)
                .then(function (result) {
                    vm.error = undefined;
                    vm.statistics = result.data;
                }, function (error) {
                    vm.error = error.data;
                    vm.statistics = [];
                });
        }
    }
})();