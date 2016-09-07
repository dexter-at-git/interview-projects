(function () {
    'use strict';

    angular
        .module('imageSearchApp', ['ui.router', 'cgBusy'])
        .config(['$urlRouterProvider', '$stateProvider', configRoutes]);

    function configRoutes($urlRouterProvider, $stateProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state('/', {
                url: '/',
                views: {
                    '': {
                        templateUrl: 'views/image-search-main.html',
                        controller: 'imageSearchController',
                        controllerAs: 'vm'
                    },
                    'results@/': {
                        templateUrl: 'views/image-search-results.html'
                    },
                    'history@/': {
                        templateUrl: 'views/image-search-history.html'
                    }
                }
            });
    }

})();