define(['angular'], function (ng) {
    var schedule = ng.module('ppts.schedule', []);

    // 配置provider
    schedule.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(schedule, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    // 配置路由
    schedule.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.schedule',
            url: '/schedule',
            templateUrl: 'app/schedule/studentassignment/stuasgmt-list/stuasgmt-list.html',
            controller: 'stuAsgmtListController',
            dependencies: ['app/schedule/studentassignment/stuasgmt-list/stuasgmt-list.controller']
        });
    });

    return schedule;
});