define([ppts.config.modules.task,
        ppts.config.dataServiceConfig.taskDataService], function (task) {
            task.registerController('completedTaskListController', [
                 'dataSyncService', 'mcsUserTaskService', 'completedTaskListDataHeader',
                function (dataSyncService, mcsUserTaskService, completedTaskListDataHeader) {
                    var vm = this;
                    vm.criteria = vm.criteria || {};
                    vm.criteria.dataType = 'completedTask';
                    vm.criteria.sendToUser = ppts.user.id;

                    dataSyncService.configDataHeader(vm, completedTaskListDataHeader, mcsUserTaskService.queryUserTasks);

                    vm.search = function () {
                        dataSyncService.initDataList(vm, mcsUserTaskService.queryUserTasks, function () {
                            dataSyncService.injectDynamicDict('processStatus');
                        });
                    };
                    vm.search();
                }]);
        });