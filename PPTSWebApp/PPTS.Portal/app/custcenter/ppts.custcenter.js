define(['angular'], function (ng) {
    var custcenter = ng.module('ppts.custcenter', []);

    // 配置provider
    custcenter.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(custcenter, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    // 配置路由
    custcenter.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.custservice',
            url: '/custservice',
            templateUrl: 'app/custcenter/custservice/custservice-list/custservice-list.html',
            controller: 'custserviceListController',
            breadcrumb: {
                label: '客户服务列表',
                parent: 'ppts'
            },
            dependencies: ['app/custcenter/custservice/custservice-list/custservice-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.custservice-add',
            url: '/custservice/add',
            templateUrl: 'app/custcenter/custservice/custservice-add/custservice-add.html',
            controller: 'custserviceAddController',
            breadcrumb: {
                label: '新增客户服务',
                parent: 'ppts.custservice'
            },
            dependencies: ['app/custcenter/custservice/custservice-add/custservice-add.controller',
                          'app/customer/potentialcustomer/customer-search/customer-search.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.custservice-view',
            url: '/custservice/view/:id?processID&activityID&resourceID',
            templateUrl: 'app/custcenter/custservice/custservice-view/custservice-view.html',
            controller: 'custserviceViewController',
            breadcrumb: {
                label: '查看客服',
                parent: 'ppts.custservice'
            },
            dependencies: ['app/custcenter/custservice/custservice-view/custservice-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.custservice-nextProcess',
            url: '/custservice/nextProcess/:id?processID&activityID&resourceID',
            templateUrl: 'app/custcenter/custservice/custservice-nextProcess/custservice-nextProcess.html',
            controller: 'custserviceNextProcessController',
            breadcrumb: {
                label: '选择下一个受理人',
                parent: 'ppts.custservice'
            },
            dependencies: ['app/custcenter/custservice/custservice-nextProcess/custservice-nextProcess.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.custservice-edit',
            url: '/custservice/edit/:id',
            templateUrl: 'app/custcenter/custservice/custservice-edit/custservice-edit.html',
            controller: 'custserviceEidtController',
            breadcrumb: {
                label: '编辑客服',
                parent: 'ppts.custservice-view'
            },
            dependencies: ['app/custcenter/custservice/custservice-edit/custservice-edit.controller']
        });
    });

    return custcenter;
});