ppts.ng.controller('appController', ['$rootScope', '$scope', '$q', '$filter', '$location', 'userService', 'utilService', 'mcsDialogService', 'mcsUserTaskService', 'storage', function ($rootScope, $scope, $q, $filter, $location, user, util, dialog, usertask, storage) {
    var vm = this;

    //vm.sidebarMenus = ppts.config.sidebarMenusConfig.forEach(function (menu) {
    //    if (menu.children && menu.children.length) {
    //        menu.children.map(function (item) {

    //        });
    //    }
    //});

    vm.sidebarMenus = ppts.config.sidebarMenusConfig;

    user.initJob(vm);

    vm.switch = function (job) {
        user.switchJob(vm, job);
    };

    vm.toggle = function () {
        vm.hideSidebar = !vm.hideSidebar;
    };

    vm.showJobFunction = function (job) {
        dialog.create('app/common/tpl/job-functions.tpl.html', {
            controller: 'jobFunctionController',
            params: {
                job: job
            }
        });
    };

    $rootScope.$on('$viewContentLoaded', function () {
        jQuery('#ppts-spinner-loading').addClass('hide');
    });

    // 待办轮询
    (function () {
        var deferred = $q.defer();
        var promise = deferred.promise;
        var timeout = null;
        var consumer = function (promise) {
            console.log('consumer is registered...');
            promise.then(null, null, function (data) {
                // 如果有新的待办任务，则发送通知提醒
                if (data && data.serverTag != ppts.config.serverTag) {
                    console.log('start notify...');
                    ppts.config.serverTag = data.serverTag;
                    util.message({
                        title: '<span><i class="ace-icon fa fa-bell icon-animated-bell"></i> 待办任务提醒</span>',
                        content: '您有新的待办任务，请及时处理！',
                    });
                }
                clearTimeout(timeout);
                polling();
            });
        }
        var polling = function () {
            console.log('start run...');
            timeout = setTimeout(function () {
                getTasks(deferred);
            }, ppts.config.taskQueryInterval);
        }
        var getTasks = function (deferred) {
            usertask.queryUserTasksAndCount({
                originalServerTag: ppts.config.serverTag,
                top: ppts.config.taskDisplayItem
            }, function (result) {
                console.log('data ready...');
                deferred.notify(result);
            });
        };

        consumer(promise);
        polling();
    })();
}]);

ppts.ng.controller('jobFunctionController', ['$uibModalInstance', 'data', function ($uibModalInstance, data) {
    var vm = this;
    vm.title = data.job.Name || '';
    vm.data = ppts.user.jobFunctions[data.job.ID];
    vm.close = function () {
        $uibModalInstance.dismiss('Canceled');
    };
}]);

ppts.ng.controller('treeController', ['$uibModalInstance', 'dataSyncService', 'data', function ($uibModalInstance, dataSyncService, data) {
    var vm = this;

    vm.title = data.title || '选择数据';
    vm.distinctLevel = data.distinctLevel;

    vm.treeSetting = dataSyncService.loadTreeSetting(data);
    // 加载树数据
    vm.loadData = function (callback) {
        dataSyncService.loadTreeData(data, callback);
    };

    vm.close = function () {
        $uibModalInstance.dismiss('Canceled');
    };

    vm.select = function () {
        $uibModalInstance.close(vm.treeSetting);
    };

}]);