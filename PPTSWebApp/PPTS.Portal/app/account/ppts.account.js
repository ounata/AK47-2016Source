define(['angular'], function (ng) {
    var account = ng.module('ppts.account', []);

    // 配置provider
    account.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(account, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    // 配置路由
    account.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            abstract: true,
            name: 'ppts.accountCharge-view',
            url: '/account/charge/view/:applyID',
            templateUrl: 'app/account/charge/charge-page/charge-view.html',
            controller: 'accountChargeViewController',
            dependencies: ['app/account/charge/charge-page/charge-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-view.info',
            url: '/info?prev=:page',
            templateUrl: 'app/account/charge/charge-page/charge-info.html',
            controller: 'accountChargeInfoController',
            breadcrumb: {
                label: '充值信息'
                },
            dependencies: ['app/account/charge/charge-page/charge-info.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-view.edit',
            url: '/edit/:path?prev=:page',
            templateUrl: 'app/account/charge/charge-page/charge-edit.html',
            controller: 'accountChargeEditController',
            breadcrumb: {
                label: '充值'
            },
            dependencies: ['app/account/charge/charge-page/charge-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-view.cert',
            url: '/cert?prev=:page',
            templateUrl: 'app/account/charge/charge-page/charge-cert.html',
            controller: 'accountChargeCertController',
            breadcrumb: {
                label: '缴费单凭证'
            },
            dependencies: ['app/account/charge/charge-page/charge-cert.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-view.payment-list',
            url: '/payment/list?prev=:page',
            templateUrl: 'app/account/charge/payment-page/payment-list.html',
            controller: 'accountChargePaymentListController',
            breadcrumb: {
                label: '登记付款并打印收据'
            },
            dependencies: ['app/account/charge/payment-page/payment-list.controller'
                         , 'app/account/charge/payment-page/payment-print.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-view.payment-edit',
            url: '/payment/edit?prev=:page',
            templateUrl: 'app/account/charge/payment-page/payment-edit.html',
            controller: 'accountChargePaymentEditController',
            breadcrumb: {
                label: '登记收款'
            },
            dependencies: ['app/account/charge/payment-page/payment-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-query',
            url: '/account/charge/query/',
            templateUrl: 'app/account/charge/charge-page/charge-query.html',
            controller: 'accountChargeQueryController',
            breadcrumb: {
                label: '缴费单列表',
                parent: 'ppts'
            },
            dependencies: ['app/account/charge/charge-page/charge-query.controller',
                           'app/account/charge/charge-page/charge-audit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountCharge-allot',
            url: '/allot/:applyID?prev=:page',
            templateUrl: 'app/account/charge/charge-page/charge-allot.html',
            controller: 'accountChargeAllotController',
            breadcrumb: {
                label: '编辑业绩归属'
            },
            dependencies: ['app/account/charge/charge-page/charge-allot.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountChargePayment-query',
            url: '/account/charge/payment/query/',
            templateUrl: 'app/account/charge/payment-page/payment-query.html',
            controller: 'accountChargePaymentQueryController',
            breadcrumb: {
                label: '收款列表',
                parent: 'ppts'
            },
            dependencies: ['app/account/charge/payment-page/payment-query.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountRefund-query',
            url: '/account/refund/query',
            templateUrl: 'app/account/refund/refund-page/refund-query.html',
            controller: 'accountRefundQueryController',
            breadcrumb: {
                label: '退费列表',
                parent: 'ppts'
            },
            dependencies: ['app/account/refund/refund-page/refund-query.controller',
                           'app/account/refund/refund-page/refund-print.controller',
                           'app/account/refund/refund-page/refund-verify.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountRefund-approve',
            url: '/account/refund/approve/:id',
            templateUrl: 'app/account/refund/refund-page/refund-approve.html',
            controller: 'accountRefundApproveController',
            breadcrumb: {
                label: '退费审批',
            },
            dependencies: ['app/account/refund/refund-page/refund-approve.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.accountTransfer-approve',
            url: '/account/transfer/approve/:id',
            templateUrl: 'app/account/transfer/transfer-page/transfer-approve.html',
            controller: 'accountTransferApproveController',
            breadcrumb: {
                label: '转让审批',
            },
            dependencies: ['app/account/transfer/transfer-page/transfer-approve.controller']
        });
    });

    return account;
});