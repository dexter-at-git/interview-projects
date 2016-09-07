(function () {
    'use strict';

    angular
        .module('transcipherApp')
        .controller('transcipherController', transcipherController);

    transcipherController.$inject = ['transcipherService'];

    function transcipherController(transcipherService) {

        var vm = this;
        vm.algorithms = [];
        vm.error = undefined;
        vm.selectedAlgorithm = undefined;
        vm.text = '';
        vm.result = '';

        initialize();

        function initialize() {
            vm.getEncryptionMethodsPromise = transcipherService.getEncryptionMethods()
                .then(function (result) {
                    vm.error = undefined;
                    vm.algorithms = result.data;
                }, function (error) {
                    vm.error = error.data;
                });
        }

        vm.encrypt = function () {
            var processingData = {
                algorithm: vm.selectedAlgorithm,
                text: vm.text
            }

            vm.encryptPromise = transcipherService.encrypt(processingData)
                .then(function (result) {
                    vm.error = undefined;
                    vm.result = result.data;
                }, function (error) {
                    vm.error = error.data;
                    vm.result = '';
                });
        }

        vm.decrypt = function () {
            var processingData = {
                algorithm: vm.selectedAlgorithm,
                text: vm.text
            }

            vm.decryptPromise = transcipherService.decrypt(processingData)
                .then(function (result) {
                    vm.error = undefined;
                    vm.result = result.data;
                }, function (error) {
                    vm.error = error.data;
                    vm.result = '';
                });
        }

        vm.algorithmChange = function () {
            vm.error = undefined;
            vm.result = '';
        }
    }
})();



