(function() {
    'use strict';


    angular.module('app')

    .config(['$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('router.one', {
                url: '/one',
                templateUrl: 'app/component/mcs-route/one.html'
            })
            .state('router.two', {
                url: '/two',
                templateUrl: 'app/component/mcs-route/two.html'
            })

        .state('router.many', {
            url: '/many',

            views: {

                'manyone': {
                    templateUrl: 'app/component/mcs-route/many.one.html'
                },
                'manytwo': {
                    templateUrl: 'app/component/mcs-route/many.two.html'
                }
            }
        })
    }])

    angular.module('app.component')



    .controller('routeController', ['$scope', function($scope) {
        var vm = this;

    }])


})();
