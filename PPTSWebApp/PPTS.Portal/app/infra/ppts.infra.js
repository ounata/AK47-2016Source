define(['angular'], function (ng) {
    var infra = ng.module('ppts.infra', []);

    // 配置provider
    infra.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(infra, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    return infra;
});