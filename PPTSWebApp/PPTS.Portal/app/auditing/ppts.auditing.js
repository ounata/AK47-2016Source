define(['angular'], function (ng) {
    var auditing = ng.module('ppts.auditing', []);

    // 配置provider
    auditing.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(auditing, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    return auditing;
});