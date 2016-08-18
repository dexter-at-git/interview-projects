(function () {
    'use strict';

    angular
        .module('testProjectsApp', ['ui.router', 'cgBusy'])
        .config(['$urlRouterProvider', '$stateProvider', configRoutes]);

    function configRoutes($urlRouterProvider, $stateProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state('/', {
                url: '/',
                templateUrl: 'description.html',
            })
            .state('image-search', {
                url: '/image-search',
                views: {
                    '': {
                        templateUrl: 'image-search/views/image-search-main.html',
                        controller: 'imageSearchController',
                        controllerAs: 'vm'
                    },
                    'results@image-search': {
                        templateUrl: 'image-search/views/image-search-results.html'
                    },
                    'history@image-search': {
                        templateUrl: 'image-search/views/image-search-history.html'
                    }
                }
            });
    }

})();