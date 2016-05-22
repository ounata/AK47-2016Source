// 配置路由
ppts.ng.run(['$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
]);
ppts.ng.config(function ($stateProvider, $urlRouterProvider) {
    // 根据当前登录人岗位确定进入工作台类别, 目前先测试咨询体系工作台
    var dashboard = {
        templateUrl: 'app/dashboard/consultant/consultant-dashboard.html',
        controller: 'consulantDashboardController',
        breadcrumb: {
            label: '咨询体系工作台',
            parent: 'ppts'
        },
        dependencies: ['app/dashboard/consultant/consultant-dashboard.controller']
    };

    mcs.util.loadDefaultRoute($stateProvider, $urlRouterProvider, {
        layout: {
            name: 'ppts',
            url: '/ppts',
            templateUrl: 'app/common/layout.html',
        },
        name: 'ppts.dashboard',
        url: '/dashboard',
        templateUrl: dashboard.templateUrl,
        controller: dashboard.controller,
        breadcrumb: dashboard.breadcrumb,
        dependencies: dashboard.dependencies
    });
});