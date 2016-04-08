define(['angular', 'mcsComponent',
        ppts.config.modules.dashboard,
        ppts.config.modules.auditing,
        ppts.config.modules.customer,
        ppts.config.modules.payment,
        ppts.config.modules.product,
        ppts.config.modules.schedule,
        ppts.config.modules.order,
        ppts.config.modules.infra,
        ppts.config.modules.custcenter,
        ppts.config.modules.contract], function (ng) {
            'use strict';

            ppts.ng = ppts.ng || ng.module('ppts', [
                'ui.router', 'ngResource', 'ngSanitize', 'blockUI', 'ui.bootstrap', 'ui.select', 'mcs.ng',
                'ppts.dashboard', 'ppts.auditing', 'ppts.customer', 'ppts.payment', 'ppts.product',
                'ppts.schedule', 'ppts.order', 'ppts.infra', 'ppts.custcenter', 'ppts.contract']);

            ppts.ng.controller('appController', ['$rootScope', '$scope', function ($rootScope, $scope) {
                var vm = this;
                vm.ssoUserIndentity = angular.element('#portalParameters').val();

                //展开当前菜单
                vm.currentMenu = {
                    show: false,
                    name: ''
                };
                vm.toggle = function (name) {
                    if (vm.currentMenu.name !== 'menu_' + name) {
                        vm.reset();
                    }
                    vm.currentMenu.name = 'menu_' + name;
                    vm.currentMenu.show = !vm.currentMenu.show;
                };
                vm.showSubMenu = function (name) {
                    return vm.currentMenu.name === 'menu_' + name && vm.currentMenu.show;
                };
                vm.reset = function () {
                    vm.currentMenu.name = '';
                    vm.currentMenu.show = false;
                };
                //最小化Sidebar
                vm.isMinimized = false;
                vm.minimizedSidebar = function () {
                    vm.isMinimized = !vm.isMinimized;
                };
            }]);

            // 配置provider
            ppts.ng.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
                mcs.util.configProvider(ppts, $controllerProvider, $compileProvider, $filterProvider, $provide);
            });
            // 配置拦截器
            ppts.ng.config(function ($httpProvider, $sceDelegateProvider) {
                mcs.util.configInterceptor($httpProvider, $sceDelegateProvider, ['timestampMarker']);
            });