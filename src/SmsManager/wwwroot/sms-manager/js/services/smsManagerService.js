(function () {
    'use strict';

    angular
        .module('smsManagerApp')
        .factory('smsManagerService', smsManagerService);

    smsManagerService.$inject = ['$http', '$httpParamSerializer'];

    function smsManagerService($http, $httpParamSerializer) {
        var service = {
            send: send,
            browse: browse,
            statistics: statistics
        };

        function send(smsMessage) {

            var params = {
                'from': smsMessage.from,
                'to': smsMessage.to,
                'text': smsMessage.message
            }

            return $http({
                method: 'GET',
                url: '../api/sms/send.json?' + $httpParamSerializer(params)
            });
        }


        function browse(browseRequest) {

            var params = {
                'dateTimeFrom': browseRequest.dateTimeFrom,
                'dateTimeTo': browseRequest.dateTimeTo,
                'skip': browseRequest.skip,
                'take': browseRequest.take
            }

            return $http({
                method: 'GET',
                url: '../api/sms/sent.json?' + $httpParamSerializer(params)
            });
        }


        function statistics(statisticsRequest) {

            var params = {
                'dateFrom': statisticsRequest.dateFrom,
                'dateTo': statisticsRequest.dateTo,
                'mccList': statisticsRequest.mccList
            }

            return $http({
                method: 'GET',
                url: '../api/sms/statistics.json?' + $httpParamSerializer(params)
            });
        }

        return service;
    }
})();