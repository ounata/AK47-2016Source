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
            url: '/account/:id',
            templateUrl: 'app/account/display/account-view/account-view.html',
            controller: 'accountController',
            dependencies: ['app/account/display/account-view/account-view.controller']
        });
    });

    return account;
});