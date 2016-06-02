define(['angular'], function (ng) {
    var infra = ng.module('ppts.infra', []);

    // 配置provider
    infra.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(infra, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    infra.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.present',
            url: '/present/list',
            templateUrl: 'app/infra/present/present-list/present-list.html',
            controller: 'presentListController',
            breadcrumb: {
                label: '买赠表管理列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/infra/present/present-list/present-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.present-add',
            url: '/present/add',
            templateUrl: 'app/infra/present/present-add/present-add.html',
            controller: 'presentAddController',
            breadcrumb: {
                label: '新赠买赠表',
                parent: 'ppts.present'
            },
            dependencies: ['app/infra/present/present-add/present-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.present-view',
            url: '/present/view/:presentId',
            templateUrl: 'app/infra/present/present-view/present-view.html',
            controller: 'presentViewController',
            breadcrumb: {
                label: '查看买赠表',
                parent: 'ppts.present'
            },
            dependencies: ['app/infra/present/present-view/present-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.present-edit',
            url: '/present/edit/:presentId',
            templateUrl: 'app/infra/present/present-edit/present-edit.html',
            controller: 'presentEditController',
            breadcrumb: {
                label: '编辑买赠表',
                parent: 'ppts.present'
            },
            dependencies: ['app/infra/present/present-edit/present-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.discount',
            url: '/discount',
            templateUrl: 'app/infra/discount/discount-list/discount-list.html',
            controller: 'discountListController',
            breadcrumb: {
                label: '折扣表管理',
                parent: 'ppts'
            },
            dependencies: ['app/infra/discount/discount-list/discount-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.discount-add',
            url: '/discount/add',
            templateUrl: 'app/infra/discount/discount-add/discount-add.html',
            controller: 'discountAddController',
            breadcrumb: {
                label: '新增折扣表',
                parent: 'ppts.discount'
            },
            dependencies: ['app/infra/discount/discount-add/discount-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.discount-view',
            url: '/discount/view/:discountId',
            templateUrl: 'app/infra/discount/discount-view/discount-view.html',
            controller: 'discountViewController',
            breadcrumb: {
                label: '查看折扣表',
                parent: 'ppts.discount'
            },
            dependencies: ['app/infra/discount/discount-view/discount-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.discount-edit',
            url: '/discount/edit/:discountId',
            templateUrl: 'app/infra/discount/discount-edit/discount-edit.html',
            controller: 'discountEditController',
            breadcrumb: {
                label: '编辑折扣表',
                parent: 'ppts.discount'
            },
            dependencies: ['app/infra/discount/discount-edit/discount-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.servicefee',
            url: '/servicefee',
            templateUrl: 'app/infra/servicefee/servicefee-list/servicefee-list.html',
            controller: 'servicefeeListController',
            breadcrumb: {
                label: '综合服务费管理',
                parent: 'ppts'
            },
            dependencies: ['app/infra/servicefee/servicefee-list/servicefee-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.servicefee-add',
            url: '/servicefee/add',
            templateUrl: 'app/infra/servicefee/servicefee-add/servicefee-add.html',
            controller: 'servicefeeAddController',
            breadcrumb: {
                label: '新增综合服务费',
                parent: 'ppts.servicefee'
            },
            dependencies: ['app/infra/servicefee/servicefee-add/servicefee-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.servicefee-edit',
            url: '/edit/:expenseID',
            templateUrl: 'app/infra/servicefee/servicefee-edit/servicefee-edit.html',
            controller: 'servicefeeEditController',
            breadcrumb: {
                label: '编辑综合服务费',
                parent: 'ppts.servicefee'
            },
            dependencies: ['app/infra/servicefee/servicefee-edit/servicefee-edit.controller']
        });
    });

    return infra;
});