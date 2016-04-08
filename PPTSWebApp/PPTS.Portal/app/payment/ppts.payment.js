define(['angular'], function (ng) {
    var payment = ng.module('ppts.payment', []);

    // 配置provider
    payment.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(payment, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    return payment;
});