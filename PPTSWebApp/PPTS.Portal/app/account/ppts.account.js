define(['angular'], function (ng) {
    var account = ng.module('ppts.account', []);

    // 配置provider
    account.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(account, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    // 配置路由
    account.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.account',
            url: '/account/:customerID',
            templateUrl: 'app/account/display/account-view/account-view.html',
            controller: 'accountController',
            breadcrumb: {
                label: '账户信息'
            },
            dependencies: ['app/account/display/account-view/account-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-edit',
            url: '/account/charge/edit/:customerID?prev=:page',
            templateUrl: 'app/account/charge/charge-edit/charge-edit.html',
            controller: 'accountChargeEditController',
            breadcrumb: {
                label: '账户充值'
            },
            dependencies: ['app/account/charge/charge-edit/charge-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-view',
            url: '/account/charge/view/:applyID',
            templateUrl: 'app/account/charge/charge-view/charge-view.html',
            controller: 'accountChargeViewController',
            breadcrumb: {
                label: '缴费单信息'
                },
            dependencies: ['app/account/charge/charge-view/charge-view.controller'
                         , 'app/account/charge/charge-view/charge-print.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountChargePayment-edit',
            url: '/account/charge/payment/edit/:applyID',
            templateUrl: 'app/account/charge/payment-edit/payment-edit.html',
            controller: 'accountChargePaymentEditController',
            breadcrumb: {
                label: '登记付款'
            },
            dependencies: ['app/account/charge/payment-edit/payment-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountChargePayment-list',
            url: '/account/charge/payment/list/:applyID',
            templateUrl: 'app/account/charge/payment-list/payment-list.html',
            controller: 'accountChargePaymentListController',
            breadcrumb: {
                label: '收款记录'
            },
            dependencies: ['app/account/charge/payment-list/payment-list.controller'
                         , 'app/account/charge/payment-list/payment-print.controller']
        });
    });

    return account;
});