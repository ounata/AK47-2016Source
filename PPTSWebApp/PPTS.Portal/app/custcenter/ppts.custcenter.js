define(['angular'], function (ng) {
    var custcenter = ng.module('ppts.custcenter', []);

    // 配置provider
    custcenter.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(custcenter, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    return custcenter;
});