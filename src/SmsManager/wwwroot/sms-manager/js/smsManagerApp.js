(function () {
    'use strict';

    angular
        .module('smsManagerApp', ['ui.router', 'cgBusy'])
        .config(['$urlRouterProvider', '$stateProvider', configRoutes]);

    function configRoutes($urlRouterProvider, $stateProvider) {

        $urlRouterProvider.otherwise('/send');

        $stateProvider
            .state('/send', {
                url: '/send',
                views: {
                    '': {
                        templateUrl: 'views/send.html',
                        controller: 'smsManagerSendController',
                        controllerAs: 'vm'
                    }
                }
            })
            .state('/browse', {
                url: '/browse',
                views: {
                    '': {
                        templateUrl: 'views/browse.html',
                        controller: 'smsManagerBrowseController',
                        controllerAs: 'vm'
                    }
                }
            })
            .state('/statistics', {
                url: '/statistics',
                views: {
                    '': {
                        templateUrl: 'views/statistics.html',
                        controller: 'smsManagerStatisticsController',
                        controllerAs: 'vm'
                    }
                }
            });
    }
})();