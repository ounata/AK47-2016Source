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
            dependencies: ['app/customer/potentialcustomer/customer-list/customer-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-add',
            url: '/customer/add',
            templateUrl: 'app/customer/potentialcustomer/customer-add/customer-add.html',
            controller: 'customerAddController',
            dependencies: ['app/customer/potentialcustomer/customer-add/customer-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-view',
            url: '/customer/view/:id/:page',
            templateUrl: 'app/customer/potentialcustomer/customer-view/customer-view.html',
            controller: 'customerViewController',
            dependencies: ['app/customer/potentialcustomer/customer-view/customer-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.follow',
            url: '/follow',
            templateUrl: 'app/customer/follow/follow-list/follow-list.html',
            controller: 'followListController',
            dependencies: ['app/customer/follow/follow-list/follow-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.customer-staffrelation',
            url: '/customer/staffrelation/:id/:type',
            templateUrl: 'app/customer/potentialcustomer/customer-staffrelation/customer-staffrelation.html',
            controller: 'customerViewController',
            dependencies: ['app/customer/potentialcustomer/customer-view/customer-view.controller']
        });
    });

    return customer;
});