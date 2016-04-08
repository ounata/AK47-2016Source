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
        if (ssoUser && ssoUser.allJobs.length) {
            ssoUser.currentRole = ssoUser.allJobs[0];
            ppts.user.currentRoleId = ssoUser.currentRole.ID;
        }
        return ssoUser;
    };

    service.switchRole = function (ssoUser, role) {
        ssoUser.currentRole = role;
        ppts.user.currentRoleId = role.ID;
    };

    return service;
});