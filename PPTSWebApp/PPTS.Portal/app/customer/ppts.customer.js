define(['angular'], function (ng) {
    var customer = ng.module('ppts.customer', []);

    // 配置provider
    customer.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(customer, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });
    // 配置路由
    customer.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.customer',
            url: '/customer',
            templateUrl: 'app/customer/potentialcustomer/customer-list/customer-list.html',
            controller: 'customerListController',
            breadcrumb: {
                label: '潜在客户列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/customer/potentialcustomer/customer-list/customer-list.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.market',
            url: '/customer/market',
            templateUrl: 'app/customer/market/market-list/market-list.html',
            controller: 'marketListController',
            breadcrumb: {
                label: '市场客户资源列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/customer/market/market-list/market-list.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-add',
            url: '/customer/add',
            templateUrl: 'app/customer/potentialcustomer/customer-add/customer-add.html',
            controller: 'customerAddController',
            breadcrumb: {
                label: '新增潜客',
                parent: 'ppts.customer'
            },
            dependencies: ['app/customer/potentialcustomer/customer-add/customer-add.controller',
                           'app/customer/potentialcustomer/customer-add/ppts.customer.relation',
                           'app/customer/potentialcustomer/customer-parent-add/parent-add.controller']
        }).loadRoute($stateProvider, {
            abstract: true,
            name: 'ppts.customer-view',
            url: '/customer/view/:id',
            templateUrl: 'app/customer/potentialcustomer/customer-view/customer-view.html',
            controller: 'customerViewController',
            dependencies: ['app/customer/potentialcustomer/customer-view/customer-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.profiles',
            url: '/profiles?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-info/customer-info.tpl.html',
            controller: 'customerInfoController',
            breadcrumb: {
                label: '查看潜客'
            },
            dependencies: ['app/customer/potentialcustomer/customer-info/customer-info.controller',
                           'app/customer/potentialcustomer/customer-parent-add/parent-add.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller',
                           'app/customer/potentialcustomer/customer-add/ppts.customer.relation']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.profile-edit',
            url: '/profiles/edit?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-edit/customer-edit.tpl.html',
            controller: 'customerEditController',
            breadcrumb: {
                label: '修改潜客'
            },
            dependencies: ['app/customer/potentialcustomer/customer-edit/customer-edit.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.parents',
            url: '/parents?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parents-view.tpl.html',
            controller: 'parentsViewController',
            breadcrumb: {
                label: '查看家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parents-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.parents-edit',
            url: '/parents/:parentId?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parent-edit.tpl.html',
            controller: 'parentEditController',
            breadcrumb: {
                label: '修改家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parent-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view.follows',
            url: '/follows',
            //templateUrl: 'app/customer/potentialcustomer/customer-view/customer-view.html'
        }).loadRoute($stateProvider, {
            name: 'ppts.follow',
            url: '/follow',
            templateUrl: 'app/customer/follow/follow-list/follow-list.html',
            controller: 'followListController',
            breadcrumb: {
                label: '跟进管理',
                parent: 'ppts'
            },
            dependencies: ['app/customer/follow/follow-list/follow-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.follow-add',
            url: '/follow/add/:customerId?prev=:page',
            templateUrl: 'app/customer/follow/follow-add/follow-add.html',
            controller: 'followAddController',
            breadcrumb: {
                label: '新增跟进记录'
            },
            dependencies: ['app/customer/follow/follow-add/follow-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-staffrelation',
            url: '/customer/staffrelation/:id/:type',
            templateUrl: 'app/customer/potentialcustomer/customer-staffrelation/customer-staffrelation.html',
            controller: 'customerStaffRelationController',
            dependencies: ['app/customer/potentialcustomer/customer-staffrelation/customer-staffrelation.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student',
            url: '/student',
            templateUrl: 'app/customer/student/student-list/student-list.html',
            controller: 'studentListController',
            breadcrumb: {
                label: '学员管理列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/customer/student/student-list/student-list.controller']
        }).loadRoute($stateProvider, {
            abstract: true,
            name: 'ppts.student-view',
            url: '/student/view/:id',
            templateUrl: 'app/customer/student/student-view/student-view.html',
            controller: 'studentViewController',
            dependencies: ['app/customer/student/student-view/student-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.profiles',
            url: '/profiles?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-info/customer-info.tpl.html',
            controller: 'customerInfoController',
            breadcrumb: {
                label: '基本信息'
            },
            dependencies: ['app/customer/potentialcustomer/customer-info/customer-info.controller',
                           'app/customer/potentialcustomer/customer-parent-add/parent-add.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller',
                           'app/customer/potentialcustomer/customer-add/ppts.customer.relation']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.parents',
            url: '/parents?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parents-view.tpl.html',
            controller: 'parentsViewController',
            breadcrumb: {
                label: '学员家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parents-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.profile-edit',
            url: '/profiles/edit?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-edit/customer-edit.tpl.html',
            controller: 'customerEditController',
            breadcrumb: {
                label: '修改学员'
            },
            dependencies: ['app/customer/potentialcustomer/customer-edit/customer-edit.controller',
                           'app/customer/potentialcustomer/customer-staff-relations/customer-staff-relations.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.student-view.parents-edit',
            url: '/parents/:parentId?prev=:page',
            templateUrl: 'app/customer/potentialcustomer/customer-parents/parent-edit.tpl.html',
            controller: 'parentEditController',
            breadcrumb: {
                label: '修改家长'
            },
            dependencies: ['app/customer/potentialcustomer/customer-parents/parent-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customermeeting',
            url: '/customermeeting',
            templateUrl: 'app/customer/customermeeting/customermeeting-list/customermeeting-list.html',
            controller: 'customerMeetingListController',
            breadcrumb: {
                label: '教学服务会',
                parent: 'ppts'
            },
            dependencies: ['app/customer/customermeeting/customermeeting-list/customermeeting-list.controller']
        });
    });

    return customer;
});