define([ppts.config.modules.task,
        ppts.config.dataServiceConfig.taskDataService], function (task) {
            task.registerController('notifyListController', [
                'dataSyncService', 'mcsUserTaskService', 'notifyListDataHeader',
                function (dataSyncService, mcsUserTaskService, notifyListDataHeader) {
                    var vm = this;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.dataType = 'notify';
                    vm.criteria.sendToUser = ppts.user.id;

                    dataSyncService.configDataHeader(vm, notifyListDataHeader, mcsUserTaskService.queryUserTasks);

                    vm.search = function () {
                        dataSyncService.initDataList(vm, mcsUserTaskService.queryUserTasks);
                    };
                    vm.search();
                }]);
        });