(function () {
    'use strict';

    angular
        .module('smsManagerApp')
        .controller('smsManagerBrowseController', smsManagerBrowseController);

    smsManagerBrowseController.$inject = ['smsManagerService'];

    function smsManagerBrowseController(smsManagerService) {

        var vm = this;
        vm.error = undefined;
        vm.response = undefined;
        vm.dateTimeFrom = '2016-09-10T11:30:20';
        vm.dateTimeTo = '2016-09-15T09:20:22';
        vm.take = 10;
        vm.skip = 0;

        vm.filter = function () {
            var smsBrowseData = {
                dateTimeFrom: vm.dateTimeFrom,
                dateTimeTo: vm.dateTimeTo,
                skip: vm.skip,
                take: vm.take
            }

            vm.browsePromise = smsManagerService.browse(smsBrowseData)
                .then(function (result) {
                    vm.error = undefined;
                    vm.response = result.data;
                }, function (error) {
                    vm.error = error.data;
                    vm.response = undefined;
                });
        }
    }
})();