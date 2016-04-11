define(['angular', 'mcsComponent',
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
                'ui.router', 'ngResource', 'ngSanitize', 'blockUI', 'ui.bootstrap', 'ui.select', 'mcs.ng',
                'ppts.dashboard', 'ppts.auditing', 'ppts.customer', 'ppts.account', 'ppts.product',
                'ppts.schedule', 'ppts.order', 'ppts.infra', 'ppts.custcenter', 'ppts.contract']);

            ppts.ng.controller('appController', ['$rootScope', '$scope', 'userService', function ($rootScope, $scope, user) {
                var vm = this;

                vm.currentUser = vm.currentUser || user.initRole();
                vm.switch = function(role) {
                    user.switchRole(vm.currentUser, role);
                };

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
// 年级过滤器
ppts.ng.filter('grade', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.grade], current);
    };
});
// 性别过滤器
ppts.ng.filter('gender', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.gender], current);
    };
});
// 证件类别过滤器
ppts.ng.filter('idtype', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.idtype], current);
    };
});
// 家庭总收入过滤器
ppts.ng.filter('income', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.income], current);
    };
});
// 孩子(男)亲属关系过滤器
ppts.ng.filter('childMale', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.childMale], current);
    };
});
// 孩子(女)亲属关系过滤器
ppts.ng.filter('childFemale', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.childFemale], current);
    };
});
// 家长(男)亲属关系过滤器
ppts.ng.filter('parentMale', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.parentMale], current);
    };
});
// 家长(女)亲属关系过滤器
ppts.ng.filter('parentFemale', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.parentFemale], current);
    };
});
// 学年过滤器
ppts.ng.filter('academicYear', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.academicYear], current);
    };
});
// vip客户类型过滤器
ppts.ng.filter('vipType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.vipType], current);
    };
});
// 客户级别过滤器
ppts.ng.filter('vipLevel', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.vipLevel], current);
    };
});
// 是否复读过滤器
ppts.ng.filter('studyAgain', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.studyAgain], current);
    };
});
// 信息来源过滤器
ppts.ng.filter('source', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.source], current);
    };
});
// 是否分配咨询师/坐席/市场专员过滤器
ppts.ng.filter('assignment', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.assignment], current);
    };
});
// 是否有效客户过滤器
ppts.ng.filter('valid', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.valid], current);
    };
});
// 跟进类型过滤器
ppts.ng.filter('followType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.followType], current);
    };
});
// 跟进阶段过滤器
ppts.ng.filter('followStage', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.followStage], current);
    };
});
// 沟通一级结果过滤器
ppts.ng.filter('mainTask', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.mainTask], current);
    };
});
// 沟通二级结果过滤器
ppts.ng.filter('subTalk', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.subTalk], current);
    };
});
// 客户级别过滤器
ppts.ng.filter('customerLevel', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.customerLevel], current);
    };
});
// 无效原因过滤器
ppts.ng.filter('invalidReason', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.invalidReason], current);
    };
});
// 购买意图过滤器
ppts.ng.filter('purchaseIntension', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.purchaseIntension], current);
    };
});
// 实际上门人数过滤器
ppts.ng.filter('verifyPeople', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.verifyPeople], current);
    };
});
// 课时时长过滤器
ppts.ng.filter('period', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.period], current);
    };
});
// 接触方式过滤器
ppts.ng.filter('contactType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.contactType], current);
    };
});
// 产品大类过滤器
ppts.ng.filter('categoryType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.categoryType], current);
    };
});
// 服务费类型过滤器
ppts.ng.filter('expenseType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.expenseType], current);
    };
});
// 年级类型过滤器
ppts.ng.filter('gradeType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.gradeType], current);
    };
});
// 科目类型过滤器
ppts.ng.filter('subjectType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.subjectType], current);
    };
});
// 科目过滤器
ppts.ng.filter('subject', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.subject], current);
    };
});
// 季节过滤器
ppts.ng.filter('season', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.season], current);
    };
});
// 课次/课时时长过滤器
ppts.ng.filter('duration', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.duration], current);
    };
});
// 课程级别过滤器
ppts.ng.filter('courseLevel', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.courseLevel], current);
    };
});
// 辅导类型过滤器
ppts.ng.filter('coachType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.coachType], current);
    };
});
// 班组类型过滤器
ppts.ng.filter('groupType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.groupType], current);
    };
});
// 班级类型过滤器
ppts.ng.filter('classType', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.classType], current);
    };
});
// 跨校区产品收入归属过滤器
ppts.ng.filter('belonging', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.belonging], current);
    };
});
// 薪酬规则对象过滤器
ppts.ng.filter('rule', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.rule], current);
    };
});
// 颗粒度过滤器
ppts.ng.filter('unit', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.unit], current);
    };
});
// 合作类型过滤器
ppts.ng.filter('hasPartner', function () {
    return function (current) {
        return mcs.util.getDictionaryItemValue(ppts.dict[ppts.config.dictMappingConfig.hasPartner], current);
    };
});
// 配置路由
ppts.ng.run(['$rootScope', '$state', '$stateParams',
    function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    }
]);
ppts.ng.config(function ($stateProvider, $urlRouterProvider) {
    mcs.util.loadDefaultRoute($stateProvider, $urlRouterProvider, {
        layout: {
            name: 'ppts',
            url: '/ppts',
            templateUrl: 'app/common/layout.html',
        },
        name: 'ppts.dashboard',
        url: '/dashboard',
        templateUrl: 'app/dashboard/dashboard.html',
        controller: 'dashboardController',
        dependencies: ['app/dashboard/dashboard.controller']
    });
});
ppts.ng.service('dataSyncService', function () {
    var service = this;

    service.initCriteria = function (vm) {
        if (!vm || !vm.data) return;
        vm.criteria = vm.criteria || {};
        vm.criteria.pageParams = vm.data.pager;
        vm.criteria.orderBy = vm.data.orderBy;
    };

    service.updateTotalCount = function (vm, result) {
        if (!vm || !vm.criteria || !result) return;
        vm.criteria.pageParams.totalCount = result.totalCount;
    };

    service.injectDictData = function (dict) {
        mcs.util.merge(dict);
    };

    service.injectPageDict = function (dictTypes) {
        for (var index in dictTypes) {
            var type = dictTypes[index];
            switch (type) {
                case 'dateRange':
                    mcs.util.merge({
                        'dateRange': [{
                            key: '1', value: '今天'
                        }, {
                            key: '2', value: '本周'
                        }, {
                            key: '3', value: '本月'
                        }, {
                            key: '4', value: '其他'
                        }]
                    });
                    break;
                case 'ifElse':
                    mcs.util.merge({
                        'ifElse': [{
                            key: '1', value: '是'
                        }, {
                            key: '0', value: '否'
                        }]
                    });
                    break;
            }
        }
    };

    service.selectPageDict = function (dictType, selectedValue) {
        if (!dictType || !selectedValue) return;
        switch (dictType) {
            case 'dateRange':
                switch (selectedValue) {
                    // 今天
                    case '1':
                        return {
                            start: new Date(),
                            end: new Date()
                        };
                        // 本周
                    case '2':
                        return {
                            start: new Date(),
                            end: new Date()
                        };
                        // 本月
                    case '3':
                        return {
                            start: new Date(),
                            end: new Date()
                        };
                        // 自定义日期
                    case '4':
                        return {
                            start: null,
                            end: null
                        };
                }
                break;
                //...
        }
    };

    service.setDefaultValue = function (vmObj, resultObj, defaultFields) {
        var fields = [];
        if (typeof defaultFields == 'string') {
            fields.push(defaultFields);
        }
        if (defaultFields instanceof Array) {
            fields = defaultFields;
        }
        if (!fields.length) return;
        for (var index in fields) {
            var field = fields[index];
            vmObj[field] = resultObj[field];
        }
    };

    return service;
});

ppts.ng.service('userService', function () {
    var service = this;

    service.initRole = function () {
        var parameters = jQuery('#portalParameters');
        if (!parameters.val()) return;
        var ssoUser = ng.fromJson(parameters.val());
        parameters.val('');
        if (ssoUser && ssoUser.jobs.length) {
            ssoUser.currentRole = ssoUser.jobs[0];
            ppts.user.currentRoleId = ssoUser.currentRole.ID;
        }
        ppts.user.functions = [];
        for (var i in ssoUser.jobs) {
            var functions = ssoUser.jobs[i].Functions;
            for (var j in functions) {
                ppts.user.functions.push(functions[j]);
            }
        }
        return ssoUser;
    };

    service.switchRole = function (ssoUser, role) {
        ssoUser.currentRole = role;
        ppts.user.currentRoleId = role.ID;
    };

    return service;
});
// 功能权限
ppts.ng.directive('pptsRoles', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsRoles) return;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                $elem.addClass('hide');
                if (mcs.util.contains(ppts.user.functions, $attrs.pptsRoles)) {
                    $elem.removeClass('hide').attr('permission', true);
                }
            }
        }
    };
});

ppts.ng.directive('pptsJobFunctions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsJobFunctions) return;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                $elem.addClass('hide');
                if (mcs.util.contains(ppts.user.functions, $attrs.pptsJobFunctions)) {
                    $elem.removeClass('hide').attr('permission', true);
                }
            }
        }
    };
});

ppts.ng.directive('pptsFunctions', function () {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsFunctions) return;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                $elem.addClass('hide');
                if (mcs.util.contains(ppts.user.functions, $attrs.pptsFunctions)) {
                    $elem.removeClass('hide').attr('permission', true);
                }
            }
        }
    };
});

ppts.ng.directive('pptsCheckboxGroup', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            model: '=',
            clear: '&?'
        },
        template: '<span><mcs-checkbox-group data="data" model="model"></mcs-checkbox-group></span>',
        link: function ($scope, $elem) {
            $scope.$on('dictionaryReady', function () {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            });
            $scope.model = $scope.model || [];
            if (angular.isFunction($scope.clear)) {
                $elem.prepend($compile(angular.element('<button class="btn btn-link" ng-click="clear()">清空</button>'))($scope));
            }
        }
    }
}]);

ppts.ng.directive('pptsRadiobuttonGroup', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            model: '='
        },
        template: '<mcs-radiobutton-group data="data" model="model"/>',
        link: function ($scope, $elem) {
            $scope.$on('dictionaryReady', function () {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            });
            $scope.model = $scope.model || '';
        }
    }
});

ppts.ng.directive('pptsSelect', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            css: '@',
            style: '@',
            caption: '@',
            model: '=',
            disabled: '@'
        },
        templateUrl: ppts.config.webportalBaseUrl + 'app/common/tpl/ctrls/select.tpl.html',
        link: function ($scope, $elem) {
            $scope.caption = $scope.caption || '请选择';
            $scope.$on('dictionaryReady', function () {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            });
            $scope.onSelectCallback = function (item, model) {
                $scope.model = model;
            };
        }
    }
});
//loading
ppts.ng.factory('timestampMarker', ["$rootScope", 'blockUI', function ($rootScope, blockUI) {
    var timestampMarker = {
        request: function (config) {
            blockUI.start();
            $rootScope.loading = true;
            config.requestTimestamp = new Date().getTime();
            return config;
        },
        response: function (response) {
            blockUI.stop();
            if (response.data && response.data.dictionaries) {
                mcs.util.merge(response.data.dictionaries);
            }
            $rootScope.loading = false;
            response.config.responseTimestamp = new Date().getTime();
            return response;
        }
    };
    return timestampMarker;
}]);
    return ppts.ng;
});