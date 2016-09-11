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
        vm.dateTimeFrom = '2015-03-01T11:30:20';
        vm.dateTimeTo = '2015-03-02T09:20:22';

        vm.filter = function () {
            var smsBrowseData = {
                dateTimeFrom: vm.dateTimeFrom,
                dateTimeTo: vm.dateTimeTo,
                skip: 10,
                take: 10
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