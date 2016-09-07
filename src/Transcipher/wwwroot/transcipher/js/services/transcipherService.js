(function () {
    'use strict';

    angular
        .module('transcipherApp')
        .factory('transcipherService', transcipherService);

    transcipherService.$inject = ['$http'];

    function transcipherService($http) {
        var service = {
            getEncryptionMethods: getEncryptionMethods,
            encrypt: encrypt,
            decrypt: decrypt
        };

        function getEncryptionMethods() {
            return $http({
                method: 'GET',
                url: '../api/transcipher/algorithms'
            });
        }

        function encrypt(encryptionData) {
            return $http({
                method: 'POST',
                url: '../api/transcipher/encrypt',
                data: JSON.stringify(encryptionData)
            });
        }

        function decrypt(encryptionData) {
            return $http({
                method: 'POST',
                url: '../api/transcipher/decrypt',
                data: JSON.stringify(encryptionData)
            });
        }

        return service;
    }
})();