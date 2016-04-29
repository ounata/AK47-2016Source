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