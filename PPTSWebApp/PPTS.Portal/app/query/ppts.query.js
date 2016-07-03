define(['angular'], function (ng) {
    var query = ng.module('ppts.query', []);

    // 配置provider
    query.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(query, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    // 配置路由
    query.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.query-student-list',
            url: '/query-student-list',
            templateUrl: 'app/query/student/student-list/student-list.html',
            controller: 'studentListController',
            breadcrumb: {
                label: '学员信息列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/query/student/student-list/student-list.controller']
        });
    });

    return query;
});