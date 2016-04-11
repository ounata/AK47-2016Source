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