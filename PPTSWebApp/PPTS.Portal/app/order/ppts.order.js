define(['angular'], function (ng) {
    var order = ng.module('ppts.order', []);

    // 配置provider
    order.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(order, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    return order;
});