define(['angular', 'mcsComponent',
        ppts.config.modules.dashboard,
        ppts.config.modules.component], function (ng) {
            'use strict';

            ppts.ng = ppts.ng || ng.module('ppts', [
                'ui.router', 'ngResource', 'ngSanitize', 'ncy-angular-breadcrumb', 'angularLocalStorage',
                'blockUI', 'ui.bootstrap', 'mcs.ng', 'ppts.dashboard', 'ppts.component']);

            // 配置provider
            ppts.ng.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide, blockUIConfig) {
                mcs.util.configProvider(ppts, $controllerProvider, $compileProvider, $filterProvider, $provide);
                blockUIConfig.autoBlock = true;
                blockUIConfig.message = '正在加载...';
                blockUIConfig.requestFilter = function(requestConfig) {
                    if (requestConfig.headers['autoComplete']) {
                        return false;
                    }
                };
            });
            // 配置拦截器
            ppts.ng.config(function ($httpProvider, $sceDelegateProvider) {
                mcs.util.configInterceptor($httpProvider, $sceDelegateProvider, ['viewLoading']);
            });
            // 配置面包屑
            ppts.ng.config(function ($breadcrumbProvider) {
                mcs.util.configBreadcrumb($breadcrumbProvider, 'samples/common/tpl/breadcrumb.tpl.html');
            });