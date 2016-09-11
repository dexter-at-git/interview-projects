(function () {
    'use strict';

    angular
        .module('smsManagerApp')
        .controller('smsManagerSendController', smsManagerSendController);

    smsManagerSendController.$inject = ['smsManagerService'];

    function smsManagerSendController(smsManagerService) {

        var vm = this;
        vm.error = undefined;
        vm.to = '+4917421293388';
        vm.from = 'The Sender';
        vm.message = 'Hello World';
        vm.result = '';

        vm.send = function () {
            var smsMessage = {
                to: vm.to,
                from: vm.from,
                message: vm.message
            }

            vm.sendPromise = smsManagerService.send(smsMessage)
                .then(function (result) {
                    vm.error = undefined;
                    vm.result = result.data;
                }, function (error) {
                    vm.error = error.data;
                    vm.result = '';
                });
        }
    }
})();