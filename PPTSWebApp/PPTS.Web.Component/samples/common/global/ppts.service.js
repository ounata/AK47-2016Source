ppts.ng.service('userService', ['storage', function (storage) {
    var service = this;

    service.sessionTokenSetter = function (token) {
        service.sessionToken = token;
    }

    service.sessionTokenGetter = function () {
        return service.sessionToken || null;
    }

    service.initJob = function (vm) {
        var ssoUser = ng.fromJson(sessionStorage.getItem('configData'));
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
        ppts.user.jobs = ssoUser.jobs;
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
    };

    service.switchJob = function (vm, job) {
        vm.currentUser.currentJob = job;
        storage.set('vm.currentUser', vm.currentUser);
        storage.set('ppts.user.currentJobId', job.ID);
    };

    return service;
}]);