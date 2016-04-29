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
                'ui.router', 'ngResource', 'ngSanitize', 'ncy-angular-breadcrumb', 
                'blockUI', 'ui.bootstrap', 'ui.select', 'mcs.ng', 'ngTagsInput',
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
ppts.ng.controller('appController', ['$rootScope', '$scope', 'userService', 'utilService', function ($rootScope, $scope, user, util) {
    var vm = this;

    vm.currentUser = vm.currentUser || user.initJob();
    vm.switch = function (job) {
        user.switchJob(vm.currentUser, job);
    };

    util.fixSidebar(vm);
}]);

ppts.ng.controller('treeController', ['$uibModalInstance', 'dataSyncService', 'data', function ($uibModalInstance, dataSyncService, data) {
    var vm = this;

    vm.title = data.title || '选择数据';

    vm.treeSetting = dataSyncService.loadTreeSetting(vm, data);
    // 加载树数据
    vm.loadData = function (callback) {
        dataSyncService.loadTreeData(vm, data, callback);
    };

    vm.close = function () {
        $uibModalInstance.dismiss('Canceled');
    };

    vm.select = function () {
        $uibModalInstance.close(vm.treeSetting);        
    };

}]);
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
        breadcrumb: {
            label: '工作台',
            parent: 'ppts'
        },
        dependencies: ['app/dashboard/dashboard.controller']
    });
});
//注册过滤器
for (var mappingCongfig in ppts.config.dictMappingConfig) {
    (function () {
        var filterName = mappingCongfig;
        ppts.ng.filter(filterName, function () {
            var config = ppts.config.dictMappingConfig[filterName];
            return function (current, separator) {
                current += '';
                if (!mcs.util.bool(current, true)) return '';
                separator = separator || ',';
                if ((current).indexOf(separator) == -1) {
                    return mcs.util.getDictionaryItemValue(ppts.dict[config], current);
                } else {
                    var array = mcs.util.toArray(current, separator);
                    if (!array.length) return '';
                    var result = mcs.util.getDictionaryItemValue(ppts.dict[config], array[0]);
                    return result ? result + '...' : '';
                }
            };
        });

        ppts.ng.filter(filterName + '_full', function () {
            var config = ppts.config.dictMappingConfig[filterName];
            return function (current, separator) {
                if (!mcs.util.bool(current)) return '';
                separator = separator || ',';
                var array = mcs.util.toArray(current, separator);
                if (!array.length) return '';
                var result = [];
                for (var item in array) {
                    if (!mcs.util.bool(array[item], true)) continue;
                    result.push(mcs.util.getDictionaryItemValue(ppts.dict[config], array[item]));
                }
                return result.join('，');
            };
        });
    })();
}
ppts.ng.service('dataSyncService', ['$resource', 'mcsDialogService', function ($resource, mcsDialogService) {
    var service = this;
    var resource = $resource(ppts.config.mcsApiBaseUrl + 'api/usergraph/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

    /**isClear, 是否清空选中项，默认为true*/
    service.initCriteria = function (vm, isClear) {
        if (!vm || !vm.data) return;
        isClear = isClear || true;
        vm.criteria = vm.criteria || {};
        if (isClear) {
            vm.data.rowsSelected = [];
        }
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
                            key: '0', value: '全部'
                        }, {
                            key: '1', value: '今天'
                        }, {
                            key: '2', value: '本周'
                        }, {
                            key: '3', value: '本月'
                        }, {
                            key: '4', value: '本季度'
                        }, {
                            key: '5', value: '其他'
                        }]
                    });
                    break;
                case 'period':
                    mcs.util.merge({
                        'period': [{
                            key: '0', value: '全部'
                        }, {
                            key: '1', value: '7天未跟进'
                        }, {
                            key: '2', value: '15天未跟进'
                        }, {
                            key: '3', value: '30天未跟进'
                        }, {
                            key: '4', value: '60天未跟进'
                        }, {
                            key: '5', value: '其他'
                        }]
                    });
                    break;
                case 'people':
                    mcs.util.merge({
                        'people': [{
                            key: '0', value: '自己'
                        }, {
                            key: '1', value: '总呼叫中心'
                        }, {
                            key: '2', value: '分呼叫中心'
                        }, {
                            key: '3', value: '分/校市场专员'
                        }, {
                            key: '4', value: '校教育咨询部'
                        }, {
                            key: '5', value: '学管部'
                        }]
                    });
                    break;
                case 'ifElse':
                    mcs.util.merge({
                        'ifElse': [{
                            key: '1', value: '是'
                        }, {
                            key: '2', value: '否'
                        }]
                    });
                    break;
            }
        }
    };

    service.selectPageDict = function (dictType, selectedValue) {
        if (!dictType || !mcs.util.bool(selectedValue, true)) return;
        switch (dictType) {
            case 'dateRange':
                switch (selectedValue) {
                    // 今天
                    case '1':
                        return {
                            start: mcs.date.today(),
                            end: mcs.date.today()
                        };
                        // 本周
                    case '2':
                        return {
                            start: mcs.date.getWeekStartDate(),
                            end: mcs.date.getWeekEndDate()
                        };
                        // 本月
                    case '3':
                        return {
                            start: mcs.date.getMonthStartDate(),
                            end: mcs.date.getMonthEndDate()
                        };
                        // 本季度
                    case '4':
                        return {
                            start: mcs.date.getQuarterStartDate(),
                            end: mcs.date.getQuarterEndDate()
                        };
                        // 全部
                        // 自定义日期
                    case '0':
                    case '5':
                        return {
                            start: null,
                            end: null
                        };
                }
                break;
            case 'studentRange':
                switch (selectedValue) {
                    // 截止到当前
                    case '0':
                        return {
                            start: null,
                            end: mcs.date.today()
                        };
                        // 本月
                    case '1':
                        return {
                            start: mcs.date.getMonthStartDate(),
                            end: mcs.date.getMonthEndDate()
                        };
                        // 最近一个月
                    case '2':
                        return {
                            start: mcs.date.getLastMonthToday(),
                            end: mcs.date.today()
                        };
                }
                break;
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

    /*
    * 弹出树控件
    */
    service.popupTree = function (vm, params) {
        mcsDialogService.create('app/common/tpl/ctrls/tree.tpl.html', {
            controller: 'treeController',
            params: params
        }).result.then(function (treeSettings) {
            vm.nodes1 = treeSettings.getNodesChecked();
            vm.nodes = treeSettings.getRawNodesChecked();
            vm.ids = treeSettings.getIdsOfNodesChecked();
            vm.names = treeSettings.getNamesOfNodesChecked();
        });
    };

    /*
    * 加载树的基本设置项
    */
    service.loadTreeSetting = function (vm, options) {
        options = angular.extend({
            data: {
                key: {
                    children: options.children || 'children',
                    name: options.name || 'name',
                    title: options.name || 'name',
                }
            },
            view: {
                selectedMulti: true,
                showIcon: true,
                showLine: true,
                nameIsHTML: false,
                fontCss: {}
            },
            check: {
                enable: true,
                chkStyle: 'checkbox'
            },
            async: {
                enable: true,
                autoParam: ["id"],
                contentType: "application/json",
                type: 'post',
                otherParam: { listMark: options.type || '0', showDeletedObjects: options.hidden || false },
                url: ppts.config.mcsApiBaseUrl + 'api/usergraph/getChildren'
            }
        }, options);

        return options;
    };

    /*
    * 调用方式: 
    service.loadTreeData(vm, {
        root: '', // 根节点名称, 默认为空, 如: '机构人员', '机构人员\总公司'
        type: 0, // 仅限于以下值，默认为0（0-None,1-Organization,2-User,4-Group,8-Sideline,15-All)
        hidden: true, // 隐藏删除的节点, 默认为true
    }, callback);
    */
    service.loadTreeData = function (vm, options, callback) {
        options = angular.extend({
            root: '',
            type: 0,
            hidden: true,
        }, options);
        if (!options.root) {
            resource.query({ operation: 'getRoot' }, function (result) {
                callback(result);
            });
        } else {
            resource.post({ operation: 'getRoot?fullPath=' + encodeURIComponent(options.root.replace('\\', '\\\\')) }, { listMark: options.type, showDeletedObjects: options.hidden }, function (result) {
                callback(result);
            });
        }
    };

    /*
    * 调用方式: 
    service.loadChildData(panretId, {
        type: 0, // 仅限于以下值，默认为0（0-None,1-Organization,2-User,4-Group,8-Sideline,15-All)
        hidden: true, // 隐藏删除的节点, 默认为true
        success: function (result) { }, // 数据加载成功回调
        error: function (error) { } // 数据加载失败回调
    });
    */
    service.loadChildData = function (parentId, options) {
        options = angular.extend({
            type: 0,
            hidden: true,
            success: null,
            error: null
        }, options);
        if (!parentId) return;
        resource.post({ operation: 'getChildren?parentId=' + encodeURIComponent(parentId.replace('\\', '\\\\')) }, { listMark: options.type, showDeletedObjects: options.hidden }, options.success, options.error);
    };

    return service;
}]);

ppts.ng.service('userService', function () {
    var service = this;

    service.initJob = function () {
        var parameters = jQuery('#portalParameters');
        if (!parameters.val()) return;
        var ssoUser = ng.fromJson(parameters.val());
        //parameters.val('');
        if (ssoUser && ssoUser.jobs.length) {
            ssoUser.currentJob = ssoUser.jobs[0];
            ppts.user.currentJobId = ssoUser.currentJob.ID;
        }
        ppts.user.roles = ssoUser.roles;
        ppts.user.token = ssoUser.token;
        ppts.user.functions = [];
        ppts.user.jobFunctions = [];
        for (var i in ssoUser.jobs) {
            var jobId = ssoUser.jobs[i].ID;
            var functions = ssoUser.jobs[i].Functions;
            ppts.user.jobFunctions[jobId] = functions;
            for (var j in functions) {
                ppts.user.functions.push(functions[j]);
            }
        }
        return ssoUser;
    };

    service.switchJob = function (ssoUser, job) {
        ssoUser.currentJob = job;
        ppts.user.currentJobId = job.ID;
    };

    return service;
});

ppts.ng.service('utilService', function () {
    var service = this;

    service.contains = function (data, elems, separator) {
        if (!data || !elems) return false;
        var array = mcs.util.toArray(elems, separator);
        for (var i in array) {
            if (jQuery.inArray(array[i], data) > -1) {
                return true;
            }
        }

        return false;
    };

    service.fixSidebar = function (vm) {
        if (!mcs.browser.get().msie) {
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
        }
    };

    service.selectOneRow = function (vm, message) {
        if (!vm.data.pager.totalCount) return;
        var selectedRows = vm.data.rowsSelected.length;
        var result = true;
        if (selectedRows == 0) {
            vm.errorMessage = message && message.NoSelect || '请选择一条记录进行操作！';
            result = false;
        }
        else if (selectedRows > 1) {
            vm.errorMessage = message && message.OneSelect || '只能选择一条记录进行操作！';
            result = false;
        }
        if (result) {
            vm.errorMessage = '';
        };
        return result;
    };

    service.selectMultiRows = function (vm, message) {
        if (!vm.data.pager.totalCount) return;
        var selectedRows = vm.data.rowsSelected.length;
        var result = true;
        if (selectedRows == 0) {
            vm.errorMessage = message && message.NoSelect || '请选择一条记录进行操作！';
            result = false;
        }
        if (result) {
            vm.errorMessage = '';
        };
        return result;
    };

    return service;
});
// 功能权限
/*
ppts.ng.directive('pptsRoles', ['utilService', function (util) {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsRoles) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                if (util.contains(ppts.user.roles, $attrs.pptsRoles)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
}]);

ppts.ng.directive('pptsJobFunctions', ['utilService', function (util) {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsJobFunctions) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                if (util.contains(ppts.user.jobFunctions[ppts.user.currentJobId], $attrs.pptsJobFunctions)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
}]);

ppts.ng.directive('pptsFunctions', ['utilService', function (util) {
    return {
        restrict: 'A',
        link: function ($scope, $elem, $attrs, $ctrl) {
            if (!$attrs.pptsFunctions) return;
            var hasPermisson = false;
            if (!mcs.util.hasAttr($elem, 'permission')) {
                if (util.contains(ppts.user.functions, $attrs.pptsFunctions)) {
                    $elem.attr('permission', true);
                    hasPermisson = true;
                }
                // hide or disable the feature when no permisson
                if (!hasPermisson) {
                    if (mcs.util.hasAttr($elem, 'support-disabled')) {
                        $elem.attr('disabled', 'disabled');
                    } else {
                        $elem.addClass('hide');
                    }
                }
            }
        }
    };
}]);
*/
ppts.ng.directive('pptsCheckboxGroup', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            model: '=',
            async: '@', 
            clear: '&?'
        },
        template: '<span><mcs-checkbox-group data="data" model="model"></mcs-checkbox-group></span>',
        link: function ($scope, $elem) {
            $scope.async = mcs.util.bool($scope.async || true);
            if ($scope.async) {
                $scope.$on('dictionaryReady', function () {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                });
            } else {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            }
          
            $scope.model = $scope.model || [];
            if (angular.isFunction($scope.clear)) {
                $elem.prepend($compile(angular.element('<button class="btn btn-link" ng-click="clear()">清空</button>'))($scope));
            }
        }
    }
}]);

ppts.ng.directive('pptsRadiobuttonGroup', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            showAll: '@',
            model: '=',
            async: '@'
        },
        template: '<mcs-radiobutton-group data="data" model="model"/>',
        link: function ($scope, $elem, $attrs, $ctrl) {
            $scope.showAll = mcs.util.bool($scope.showAll || false);
            $scope.async = mcs.util.bool($scope.async || true);
            if ($scope.async) {
                $scope.$on('dictionaryReady', function () {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                });
            } else {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            }
            $scope.model = $scope.model || '';
            if ($scope.showAll) {
                var groupName = $elem.children().attr('groupname');
                $elem.prepend($compile(angular.element('<label class="radio-inline"><input name="' + groupName + '" type="radio" class="ace" ng-checked="true" ng-click="model=0" checked="checked"><span class="lbl"></span> 全部</label>'))($scope));
            }
        }
    }
}]);

ppts.ng.directive('pptsSelect', function () {
    return {
        restrict: 'E',
        scope: {
            category: '@',
            css: '@',
            style: '@',
            caption: '@',
            model: '=',
            async: '@',
            callback: '&?',
            disabled: '@'
        },
        templateUrl: 'app/common/tpl/ctrls/select.tpl.html',
        link: function ($scope, $elem) {
            $scope.caption = $scope.caption || '请选择';
            $scope.async = mcs.util.bool($scope.async || true);
            if ($scope.async) {
                $scope.$on('dictionaryReady', function () {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                });
            } else {
                $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
            }
            $scope.onSelectCallback = function (item, model) {
                $scope.model = model;
                if (angular.isFunction($scope.callback)) {
                    $scope.callback({ item: item, model: model });
                }
            };
        }
    }
});

ppts.ng.directive('pptsDatepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '='
        },
        templateUrl: 'app/common/tpl/ctrls/datepicker.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.date-picker');

            $this.datepicker({
                autoclose: true,
                todayHighlight: true,
                format: ppts.config.datePickerFormat,
                language: ppts.config.datePickerLang
            }) //show datepicker when clicking on the icon
			.next().on('click', function () {
			    $(this).prev().focus();
			});
        }
    }
});

ppts.ng.directive('pptsDaterangepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            size: '@',
            startDate: '=',
            endDate: '='
        },
        templateUrl: 'app/common/tpl/ctrls/daterangepicker.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.input-daterange');

            $scope.size = $scope.size || 'lg';

            $this.datepicker({
                autoclose: true,
                todayHighlight: true,
                format: ppts.config.datePickerFormat,
                language: ppts.config.datePickerLang
            }).find('.date-picker').next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
});

ppts.ng.directive('pptsTimepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            step: '@',
            model: '='
        },
        templateUrl: 'app/common/tpl/ctrls/timepicker.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.time-picker');

            $this.timepicker({
                minuteStep: $scope.step || 30,
                showSeconds: true,
                showMeridian: false
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
});

ppts.ng.directive('pptsDatetimepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '='
        },
        templateUrl: 'app/common/tpl/ctrls/datetimepicker.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.date-timepicker');

            $this.datetimepicker({
                format: ppts.config.datetimePickerFormat,
                showSeconds: true,
                language: ppts.config.datePickerLang
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
});

ppts.ng.directive('pptsDatarange', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            min: '=',
            minText: '@',
            max: '=',
            maxText: '@'
        },
        templateUrl: 'app/common/tpl/ctrls/datarange.tpl.html',
        link: function ($scope, $elem) {
            var $this = $elem.find('.data-range');

            //$this.datetimepicker({
            //    format: ppts.config.datetimePickerFormat,
            //    showSeconds: true,
            //    language: ppts.config.datePickerLang
            //}).next().on('click', function () {
            //    $(this).prev().focus();
            //});
        }
    }
});

ppts.ng.directive('pptsSearch', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            model: '=',
            placeholder: '@',
            click: '&'
        },
        templateUrl: 'app/common/tpl/ctrls/search.tpl.html'
    }
});

ppts.ng.directive("pptsCompileHtml", ['$parse', '$sce', '$compile', function ($parse, $sce, $compile) {
    return {
        restrict: "A",
        link: function ($scope, $elem, $attrs) {
            var expression = $sce.parseAsHtml($attrs.pptsCompileHtml);
            var getResult = function () {
                return expression($scope);
            };
            $scope.$watch(getResult, function (newValue) {
                var linker = $compile(newValue);
                $elem.append(linker($scope));
            });
        }
    }
}]);
//loading
ppts.ng.factory('viewLoading', ["$rootScope", 'blockUI', '$q', function ($rootScope, blockUI, $q) {
    var viewLoading = {
        request: function (config) {
            blockUI.start();
            config.headers['pptsCurrentJobID'] = ppts.user.currentJobId;
            config.headers['requestToken'] = ppts.user.token;
            return config;
        },
        response: function (response) {
            blockUI.stop();
            if (response.data && response.data.dictionaries) {
                mcs.util.merge(response.data.dictionaries);
            }
            return response || $q.when(response);
        },
        responseError: function (error) {
            blockUI.stop();
            return $q.reject(error);
        }
    };
    return viewLoading;
}]);
    return ppts.ng;
});