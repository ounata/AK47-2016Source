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
            // 配置缓存模板
            ppts.ng.run(['$templateCache', function($templateCache) {
                mcs.util.configCacheTemplate($templateCache, 'advance-search.tpl.html', '<div id="accordion" class="accordion-style1 panel-group"><div class="panel panel-info"><div class="panel-heading"><h4 class="panel-title"><a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#advance-search"><i class="ace-icon fa fa-angle-right bigger-110" data-icon-hide="fa-angle-down" data-icon-show="fa-angle-right"></i>                    &nbsp;高级查询</a></h4></div><div class="panel-collapse collapse mcs-advance-search" id="advance-search"><div class="panel-body"><div class="profile-user-info mcs-user-profile"><div class="profile-info-row" ng-repeat="item in vm.searchItems" ng-hide="{{item.hide}}"><div class="profile-info-name">{{item.name}}</div><div class="profile-info-value" ppts-compile-html="item.template | trusted"></div></div></div></div></div></div></div>');
                mcs.util.configCacheTemplate($templateCache, 'quick-search.tpl.html', '<div class="mcs-panel-search well well-sm"><div class="portlet"><div class="portlet-title"><div class="caption"><label class="quick-search"><i class="ace-icon fa fa-search bigger-125"></i> 快速查询</label></div><div class="actions"><mcs-button category="search" click="vm.search()" size="medium" css="mcs-width-200" /></div></div><div class="portlet-body" ng-transclude></div></div></div>');
                mcs.util.configCacheTemplate($templateCache, 'toolbar.tpl.html', '<span><div class="mcs-panel-toolbar" ng-transclude></div><div class="alert alert-block alert-danger" ng-show="vm.errorMessage"><button type="button" class="close" data-dismiss="alert"><i class="ace-icon fa fa-times"></i></button><i class="ace-icon fa fa-warning"></i> {{vm.errorMessage}}</div></span>');
            }]);
            // 配置拦截器
            ppts.ng.config(function ($httpProvider, $sceDelegateProvider) {
                mcs.util.configInterceptor($httpProvider, $sceDelegateProvider, ['viewLoading']);
            });
            // 配置面包屑
            ppts.ng.config(function ($breadcrumbProvider) {
                mcs.util.configBreadcrumb($breadcrumbProvider, 'app/common/tpl/breadcrumb.tpl.html');
            });