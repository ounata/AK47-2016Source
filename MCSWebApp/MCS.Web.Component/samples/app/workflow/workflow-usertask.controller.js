(function () {
    angular.module('app.workflow').controller('WorkflowUsertaskController', ['mcsUserTaskService', 'dataLoadService', 'userTaskListDataHeader',
        function (mcsUserTaskService, dataLoadService, userTaskListDataHeader) {
            var vm = this;
            vm.criteria = vm.criteria || {};
            vm.criteria.dataType = 'userTask';
            vm.criteria.sendToUser = '19617';

            dataLoadService.configDataHeader(vm, userTaskListDataHeader, mcsUserTaskService.queryUserTasks);

            vm.search = function () {
                dataLoadService.initDataList(vm, mcsUserTaskService.queryUserTasks);
            };
            vm.search();

        }]).service('dataLoadService', function () {
            var service = this;

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
                if (vm.currentCriteria) {
                    vm.currentCriteria.pageParams = vm.criteria.pageParams;
                    vm.currentCriteria.orderBy = vm.criteria.orderBy;
                }
            };

            service.updateTotalCount = function (vm, result) {
                if (!vm || !vm.criteria || !result) return;
                vm.criteria.pageParams.totalCount = result.totalCount;
            };


            service.configDataHeader = function (vm, header, request, callback) {
                if (!vm || !header) return;
                vm.data = header;
                vm.data.orderBy = vm.data.orderBy || [];
                vm.data.pager = angular.extend({
                    pageIndex: 1,
                    pageSize: ppts.config.pageSizeItem,
                    totalCount: -1
                }, vm.data.pager);

                vm.data.pager.pageChange = function () {
                    service.initCriteria(vm);
                    if (angular.isFunction(request)) {
                        request(vm.currentCriteria, function (result) {
                            vm.data.rows = result.queryResult ? result.queryResult.pagedData : result.pagedData;
                            vm.criteria = angular.copy(vm.currentCriteria, {});
                            if (ng.isFunction(callback)) {
                                callback(result);
                            }
                        });
                    }
                }
            };

            service.initDataList = function (vm, request, callback) {
                vm.data.pager.pageIndex = 1;
                service.initCriteria(vm);
                if (angular.isFunction(request)) {
                    request(vm.criteria, function (result) {
                        vm.data.rows = result.queryResult ? result.queryResult.pagedData : result.pagedData;
                        service.updateTotalCount(vm, result.queryResult || result);
                        vm.currentCriteria = angular.copy(vm.criteria, {});
                        if (ng.isFunction(callback)) {
                            callback(result);
                        }
                    });
                }
            };

            return service;
        }).constant('userTaskListDataHeader', {
            headers: [{
                field: "taskTitle",
                sort: 'TASK_TITLE',
                name: "标题",
                template: '<a href="{{row.normalizedUrl}}">{{row.taskTitle}}</a>',
                sortable: true
            }, {
                field: "sourceName",
                sort: 'SOURCE_NAME',
                name: "发送人",
                sortable: true
            }, {
                field: "sendToUserName",
                name: "发送至"
            }, {
                field: "draftDepartmentName",
                name: "起草部门"
            }, {
                field: "deliverTime",
                sort: 'DELIVER_TIME',
                name: "任务发送时间",
                template: '<span>{{row.deliverTime|date:"yyyy-MM-dd HH:mm:ss"|normalize}}</span>',
                sortable: true
            }, {
                field: "expireTime",
                sort: 'EXPIRE_TIME',
                name: "过期时间",
                template: '<span>{{row.expireTime|date:"yyyy-MM-dd HH:mm:ss"|normalize}}</span>',
                sortable: true
            }],
            orderBy: [{ dataField: 'DELIVER_TIME', sortDirection: 1 }]
        });
})();