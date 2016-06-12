define(['angular'], function (ng) {
    var component = ng.module('ppts.component', []);

    // 配置provider
    component.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(component, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });
 
    return component;
});