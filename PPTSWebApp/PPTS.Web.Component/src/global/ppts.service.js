ppts.ng.service('dataSyncService', ['$resource', 'mcsDialogService', function ($resource, mcsDialogService) {
    var service = this;
    var resource = $resource(ppts.config.pptsApiBaseUrl + 'api/organization/:operation/:id',
         { operation: '@operation', id: '@id' },
         {
             'post': { method: 'POST' },
             'query': { method: 'GET', isArray: false }
         });

    /**isClear, 是否清空选中项，默认为true*/
    service.initCriteria = function (vm, isClear) {
        if (!vm || !vm.data) return;
        if (isClear == undefined) isClear = true;
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
                            key: '0', value: '否'
                        }]
                    });
                    break;
                case 'messageType':
                    mcs.util.merge({
                        'messageType': [{
                            key: '1', value: '发送邮件'
                        }, {
                            key: '2', value: '发送短信'
                        }]
                    });
                    break;
                case 'week':
                    mcs.util.merge({
                        'week': [{
                            key: '1', value: '星期一'
                        }, {
                            key: '2', value: '星期二'
                        }, {
                            key: '3', value: '星期三'
                        }, {
                            key: '4', value: '星期四'
                        }, {
                            key: '5', value: '星期五'
                        }, {
                            key: '6', value: '星期六'
                        }, {
                            key: '7', value: '星期日'
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
    * 加载树的基本设置项
    */
    service.loadTreeSetting = function (options) {
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
                otherParam: { listMark: options.type || '15', showDeletedObjects: options.hidden || false },
                url: ppts.config.pptsApiBaseUrl + 'api/organization/getdatascopechildren'
            }
        }, options);

        return options;
    };

    /*
    * 调用方式: 
    service.loadTreeData({
        root: '', // 根节点名称, 默认为空, 如: '机构人员', '机构人员\总公司'
        type: 0, // 仅限于以下值，默认为0（0-None,1-Organization,2-User,4-Group,8-Sideline,15-All)
        hidden: true, // 隐藏删除的节点, 默认为true
    }, callback);
    */
    service.loadTreeData = function (options, callback) {
        resource.post({ operation: 'getdatascoperoot' }, { id: options.id, fullPath: options.root, listMark: options.type, showDeletedObjects: options.hidden }, function (result) {
            callback(result);
        });
    };

    return service;
}]);

ppts.ng.service('userService', ['storage', function (storage) {
    var service = this;

    service.initJob = function (vm) {
        var parameters = jQuery('#portalParameters');
        if (!parameters.val()) return;
        var ssoUser = ng.fromJson(parameters.val());
        var currentUser = storage.get('vm.currentUser');
        if (currentUser && ssoUser.userId != currentUser.userId) {
            storage.remove('vm.currentUser');
            storage.remove('ppts.user.currentJobId');
        }
        vm.currentUser = currentUser;
        if (!vm.currentUser) {
            vm.currentUser = ssoUser;
            if (ssoUser && ssoUser.jobs.length) {
                vm.currentUser.currentJob = ssoUser.jobs[0];
                storage.set('ppts.user.currentJobId', ssoUser.jobs[0].ID);
            }
            storage.set('vm.currentUser', vm.currentUser);
        }
        ppts.user.id = ssoUser.userId;
        ppts.user.name = ssoUser.displayName;
        ppts.user.orgId = ssoUser.orgId;
        ppts.user.roles = ssoUser.roles;
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
    };

    service.switchJob = function (vm, job) {
        vm.currentUser.currentJob = job;
        storage.set('vm.currentUser', vm.currentUser);
        storage.set('ppts.user.currentJobId', job.ID);
    };

    return service;
}]);

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