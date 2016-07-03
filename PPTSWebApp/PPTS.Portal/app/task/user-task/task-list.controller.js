define([ppts.config.modules.task,
        ppts.config.dataServiceConfig.taskDataService], function (task) {
            task.registerController('userTaskListController', [
                'dataSyncService', 'mcsUserTaskService', 'userTaskListDataHeader',
                function (dataSyncService, mcsUserTaskService, userTaskListDataHeader) {
                    var vm = this;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.dataType = 'userTask';
                    vm.criteria.sendToUser = ppts.user.id;

                    dataSyncService.configDataHeader(vm, userTaskListDataHeader, mcsUserTaskService.queryUserTasks);

                    vm.search = function () {
                        dataSyncService.initDataList(vm, mcsUserTaskService.queryUserTasks);
                    };
                    vm.search();
                }]);
        });