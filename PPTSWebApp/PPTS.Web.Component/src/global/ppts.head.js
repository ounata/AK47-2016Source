define(['angular', 'mcsComponent', 'autocomplete',
        ppts.config.modules.dashboard,
        ppts.config.modules.auditing,
        ppts.config.modules.customer,
        ppts.config.modules.account,
        ppts.config.modules.product,
        ppts.config.modules.schedule,
        ppts.config.modules.order,
        ppts.config.modules.infra,
        ppts.config.modules.custcenter,
        ppts.config.modules.contract], function (ng) {
            'use strict';

            ppts.ng = ppts.ng || ng.module('ppts', [
                'ui.router', 'ngResource', 'ngSanitize', 'ncy-angular-breadcrumb', 'angularLocalStorage',
                'blockUI', 'ui.bootstrap', 'mcs.ng', 'ngTagsInput', 'ngFileUpload',
                'ppts.dashboard', 'ppts.auditing', 'ppts.customer', 'ppts.account', 'ppts.product', 
                'ppts.schedule', 'ppts.order', 'ppts.infra', 'ppts.custcenter', 'ppts.contract']);

            // 配置provider
            ppts.ng.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
                mcs.util.configProvider(ppts, $controllerProvider, $compileProvider, $filterProvider, $provide);
            });
            // 配置拦截器
            ppts.ng.config(function ($httpProvider, $sceDelegateProvider) {
                mcs.util.configInterceptor($httpProvider, $sceDelegateProvider, ['viewLoading']);
            });
            // 配置面包屑
            ppts.ng.config(function ($breadcrumbProvider) {
                mcs.util.configBreadcrumb($breadcrumbProvider, 'app/common/tpl/breadcrumb.tpl.html');
            });