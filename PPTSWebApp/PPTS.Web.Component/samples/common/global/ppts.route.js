// 配置路由
ppts.ng.run(['$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
]);
ppts.ng.config(function ($stateProvider, $urlRouterProvider) {
    var dashboard = {
        templateUrl: 'samples/dashboard/main.html',
        controller: 'dashboardController',
        breadcrumb: {
            label: '首页',
            parent: 'ppts'
        },
        dependencies: ['samples/dashboard/dashboard.controller']
    };

    mcs.util.loadDefaultRoute($stateProvider, $urlRouterProvider, {
        layout: {
            name: 'ppts',
            url: '/ppts',
            templateUrl: 'samples/common/layout.html',
        },
        name: 'ppts.dashboard',
        url: '/dashboard',
        templateUrl: dashboard.templateUrl,
        controller: dashboard.controller,
        breadcrumb: dashboard.breadcrumb,
        dependencies: dashboard.dependencies
    });
});