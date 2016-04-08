define(['angular'], function (ng) {
    var contract = ng.module('ppts.contract', []);

    // 配置provider
    contract.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(contract, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    return contract;
});