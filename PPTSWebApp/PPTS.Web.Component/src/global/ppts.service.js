ppts.ng.service('dataSyncService', ['$resource', function($resource) {
    var service = this;
    var resource = $resource(ppts.config.pptsApiBaseUrl + 'api/organization/:operation/:id', {
        operation: '@operation',
        id: '@id'
    }, {
        'post': {
            method: 'POST'
        },
        'query': {
            method: 'GET',
            isArray: false
        }
         });

    /**isClear, 是否清空选中项，默认为true*/
    service.initCriteria = function(vm, isClear) {
        if (!vm || !vm.data) return;
        if (isClear == undefined) isClear = true;
        vm.criteria = vm.criteria || {};
        if (isClear) {
            vm.data.rowsSelected = [];
        }
        vm.criteria.pageParams = vm.data.pager;
        vm.criteria.orderBy = vm.data.orderBy;
        if (vm.currentCriteria) {
            vm.currentCriteria.pageParams = vm.criteria.pageParams;
            vm.currentCriteria.orderBy = vm.criteria.orderBy;
        }
    };

    service.updateTotalCount = function (vm, result) {
        if (!vm || !vm.criteria || !result) return;
        vm.criteria.pageParams.totalCount = result.totalCount;
    };

    service.injectDictData = function(dict) {
        mcs.util.merge(dict);
    };

    service.injectDynamicDict = function(data, options) {
        if (!data) return;
        var dict = service.loadDynamicDict(data);
        var inject = function(items, category) {
            var kvp = {
                key: 'key',
                value: 'value'
            };
            var opts = angular.extend(kvp, options);
            var categoryKey = category + (opts.keyName ? '_' + opts.keyName : '');
            var categoryName = 'c_codE_ABBR_' + categoryKey;
            var categoryValue = mcs.util.mapping(items, opts, categoryKey);
            ppts.config.dictMappingConfig[categoryKey] = categoryName;
            service.injectDictData(categoryValue);
        };
        if (dict) {
            for (var item in dict) {
                inject(dict[item], item);
            }
        } else {
            if (!options.category) return;
            inject(data, options.category);
        }
    };

    service.distroyDynamicDict = function(category, keyName) {
        var categoryKey = category + (keyName ? '_' + keyName : '');
        var categoryName = 'c_codE_ABBR_' + categoryKey;
        delete mcs.app.dict[categoryName];
    };

    service.loadDynamicDict = function(dictTypes) {
        var dict = null;
        if (mcs.util.isString(dictTypes)) {
            dict = dict || {};
            var array = mcs.util.toArray(dictTypes);
            for (var index in array) {
                dict[array[index]] = ppts.enum[array[index]];
            }
        }
        return dict;
    };

    service.configDataHeader = function(vm, header, request, callback) {
        if (!vm || !header) return;
        vm.data = header;
        vm.data.orderBy = vm.data.orderBy || [];
        vm.data.pager = angular.extend({
            pageIndex: 1,
            pageSize: ppts.config.pageSizeItem,
            totalCount: -1
        }, vm.data.pager);

        vm.data.pager.pageChange = function() {
            service.initCriteria(vm);
            if (angular.isFunction(request)) {
                request(vm.currentCriteria, function(result) {
                    vm.data.rows = result.queryResult ? result.queryResult.pagedData : result.pagedData;
                    vm.criteria = angular.copy(vm.currentCriteria, {});
                    if (ng.isFunction(callback)) {
                        callback(result);
                    }
                });
            }
        }
    };

    service.initDataList = function(vm, request, callback) {
        vm.data.pager.pageIndex = 1;
        service.initCriteria(vm);
        if (angular.isFunction(request)) {
            request(vm.criteria, function(result) {
                vm.data.rows = result.queryResult ? result.queryResult.pagedData : result.pagedData;
                service.updateTotalCount(vm, result.queryResult || result);
                vm.currentCriteria = angular.copy(vm.criteria, {});
                if (ng.isFunction(callback)) {
                    callback(result);
                }
            });
        }
    };

    service.selectPageDict = function(dictType, selectedValue) {
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
                    case '-1':
                    case '5':
                        return {
                            start: null,
                            end: null
                        };
                }
                break;
            case 'period':
                switch (selectedValue) {
                    // 全部
                    // 其他
                    case '-1':
                    case '5':
                        return {
                            end: null
                        };
                        // 7天未跟进
                    case '1':
                        return {
                            end: mcs.date.lastDay(-7)
                        };
                        // 15天未跟进
                    case '2':
                        return {
                            end: mcs.date.lastDay(-15)
                        };
                        // 30天未跟进
                    case '3':
                        return {
                            end: mcs.date.lastDay(-30)
                        };
                        // 60天未跟进
                    case '4':
                        return {
                            end: mcs.date.lastDay(-60)
                        };
                }
                break;
            case 'studentRange':
                switch (selectedValue) {
                    // 全部
                    case '-1':
                        return {
                            start: null,
                            end: null
                        };
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
                            start: mcs.date.lastDay(-30),
                            end: mcs.date.today()
                        };
                }
                break;
        }
    };

    service.setDefaultValue = function(vmObj, resultObj, defaultFields) {
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
    service.loadTreeSetting = function(options) {
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
                chkStyle: options.selection || 'checkbox',
                radioType: 'all'
            },
            async: {
                enable: true,
                autoParam: ["id"],
                contentType: "application/json",
                type: 'post',
                otherParam: {
                    listMark: options.type || '15',
                    showDeletedObjects: options.hidden || false
                },
                url: ppts.config.pptsApiBaseUrl + 'api/organization/getdatascopechildren'
            }
        }, options);

        if (options.distinctLevel) {
            options.callback = options.callback || {};
            options.callback.beforeCheck = function(treeId, treeNode) {
                return options.justCheckWithinSameParent(treeId, treeNode);
            };
        }

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
    service.loadTreeData = function(options, callback) {
        resource.post({
            operation: 'getdatascoperoot'
        }, {
            id: options.id,
            fullPath: options.root,
            listMark: options.type,
            showDeletedObjects: options.hidden
        }, function(result) {
            callback(result);
        });
    };

    return service;
}]);

ppts.ng.service('exportExcelService', ['storage', function(storage) {
    var service = this;

    var currentJobId = storage.get('ppts.user.currentJobId_' + ppts.user.id);
    service.export = function(url, params) {
        params = angular.extend({
            pptsCurrentJobID: currentJobId
        }, params);
        mcs.util.postMockForm(url, params);
    }

    return service;
}]);

ppts.ng.service('userService', ['storage', function(storage) {
    var service = this;

    service.sessionTokenSetter = function(token) {
        service.sessionToken = token;
    }

    service.sessionTokenGetter = function() {
        return service.sessionToken || null;
    }

    service.initJob = function(vm) {
        var ssoUser = ng.fromJson(document.getElementById('configData').value);

        if (ssoUser) {
            vm.currentUser = angular.copy(ssoUser, {});
            if (ssoUser.jobs.length) {
                // 检测上次的岗位是否在最新的岗位列表中，如未在则返回第一个默认岗位
                var lastJobId = storage.get('ppts.user.currentJobId_' + ssoUser.userId);
                if (!lastJobId || !mcs.util.containsObject(ssoUser.jobs, {
                        ID: lastJobId
                    }, 'ID')) {
                    vm.currentUser.currentJob = ssoUser.jobs[0];
                    storage.set('ppts.user.currentJobId_' + ssoUser.userId, ssoUser.jobs[0].ID);
                } else {
                    vm.currentUser.currentJob = ssoUser.jobs.filter(function(job) {
                        return job.ID == lastJobId;
                    })[0];
                }
            }
            // 保存当前登录用户的信息
            ppts.user.id = ssoUser.userId;
            ppts.user.name = ssoUser.displayName;
            ppts.user.logOnName = ssoUser.logOnName;
            ppts.user.orgId = ssoUser.orgId;
            ppts.user.branchId = ssoUser.branchId;
            ppts.user.campusId = ssoUser.campusId;
            ppts.user.roles = ssoUser.roles;
            ppts.user.jobs = ssoUser.jobs;
            ppts.user.token = ssoUser.token;

            vm.logoffUrl = ssoUser.logoffUrl;
            vm.userTasks = ssoUser.userTasksAndCount;

            ppts.config.serverTag = vm.userTasks.ServerTag;
            ppts.config.enablePermission = ssoUser.roleCheckEnabled;

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
        }
    };

    service.switchJob = function(vm, job) {
        vm.currentUser.currentJob = job;
        storage.set('ppts.user.currentJobId_' + ppts.user.id, job.ID);
    };

    return service;
}]);

ppts.ng.service('utilService', function() {
    var service = this;

    service.showMessage = function(vm, condition, message) {
        var result = mcs.util.bool(condition);
        if (result) {
            vm.errorMessage = message || '选择的记录不满足条件！';
        } else {
            vm.errorMessage = '';
        }

        return result;
    };

    service.selectOneRow = function(vm, message) {
        var selectedRows = vm.data.rowsSelected.length;
        var result = true;
        if (selectedRows == 0) {
            vm.errorMessage = message && message.NoSelect || '请选择一条记录进行操作！';
            result = false;
        } else if (selectedRows > 1) {
            vm.errorMessage = message && message.OneSelect || '只能选择一条记录进行操作！';
            result = false;
        }
        if (result) {
            vm.errorMessage = '';
        };
        return result;
    };

    service.selectMultiRows = function(vm, message) {
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

    service.message = function(options) {
        var opts = jQuery.extend({
            title: '消息提醒',
            content: '您有一条新的消息，请查收！',
            messageType: 'info',
            sticky: false
        }, options);

        jQuery.gritter.add({
            title: opts.title,
            sticky: opts.sticky,
            text: opts.content,
            class_name: 'gritter-' + opts.messageType
        });
    };

    return service;
});