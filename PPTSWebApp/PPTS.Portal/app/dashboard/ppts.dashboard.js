define(['angular'], function (ng) {
    var dashboard = ng.module('ppts.dashboard', []);

    // 配置provider
    dashboard.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(dashboard, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });
 
    return dashboard;
});