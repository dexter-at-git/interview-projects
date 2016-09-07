(function () {
    'use strict';

    angular
        .module('transcipherApp', ['ui.router', 'cgBusy'])
        .config(['$urlRouterProvider', '$stateProvider', configRoutes]);

    function configRoutes($urlRouterProvider, $stateProvider) {

        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state('/', {
                url: '/',
                views: {
                    '': {
                        templateUrl: 'views/transcipher.html',
                        controller: 'transcipherController',
                        controllerAs: 'vm'
                    }
                }
            });
    }

})();