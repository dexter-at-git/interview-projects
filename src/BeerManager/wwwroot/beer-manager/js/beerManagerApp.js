(function () {
    'use strict';

    angular
        .module('beerManagerApp', ['ui.router', 'cgBusy'])
        .config(['$urlRouterProvider', '$stateProvider', configRoutes]);

    function configRoutes($urlRouterProvider, $stateProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state('/', {
                url: '/',
                views: {
                    '': {
                        templateUrl: 'views/beerManager.html',
                        controller: 'beerManagerController',
                        controllerAs: 'vm'
                    }
                }
            });
    }
})();