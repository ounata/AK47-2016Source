// 配置路由
ppts.ng.run(['$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
]);
ppts.ng.config(function ($stateProvider, $urlRouterProvider) {
    mcs.util.loadDefaultRoute($stateProvider, $urlRouterProvider, {
        layout: {
            name: 'ppts',
            url: '/ppts',
            templateUrl: 'app/common/layout.html',
        },
        name: 'ppts.dashboard',
        url: '/dashboard',
        templateUrl: 'app/dashboard/dashboard.html',
        controller: 'dashboardController',
        dependencies: ['app/dashboard/dashboard.controller']
    });
});