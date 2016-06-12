(function() {
    angular.module('app.workflow')

    .controller('WorkflowFormController', ['$scope', '$state', '$stateParams', 'mcsWorkflowService', function ($scope, $state, $stateParams, mcsWorkflowService) {
        var vm = this;

        vm.comment = '';

        vm.searchParams = {
            processID: $stateParams.pid,
            activityID: $stateParams.aid,
            resourceID: $stateParams.rid
        };

        vm.approvalHistoryData = {
            keyFields: ['activityID'],
            orderBy: [{
                dataField: 'approvalTime',
                sortDirection: 1
            }],
            headers: [{
                field: "approvalTime",
                name: "操作时间",
                headerCss: 'datatable-header-align-center',
                sortable: false,
                description: '操作时间',
                template: '<span>{{row.approvalTime | date: "yyyy-MM-dd HH:mm"}}</span>'
            },

            {
                field: "activityName",
                name: "环节名",
                headerCss: 'datatable-header-align-center',
                sortable: false,
                description: '环节名'
            },

            {
                field: "approver",
                name: "操作者",
                headerCss: 'datatable-header-align-center',
                sortable: false,
                description: '操作者'
            },

            {
                field: "action",
                name: "操作",
                headerCss: 'datatable-header-align-center',
                sortable: false,
                description: '操作'
            },

            {
                field: "comment",
                name: "处理意见",
                headerCss: 'datatable-header-align-left',
                sortable: false,
                description: '处理意见'
            },

            {
                field: "approvalElapsedTime",
                name: "花费时间",
                headerCss: 'datatable-header-align-center',
                sortable: false,
                description: '花费时间'
            }],
            rows: [],
            pager: {
                noPager: true
            }
        };

        mcsWorkflowService.getClientProcess(vm.searchParams, function (result) {
            vm.clientProcess = result;
            vm.approvalHistoryData.rows = vm.clientProcess.activities.filter(function (item) {
                return item.activityStatus == 'Completed' || item.activityStatus == 'Aborted';
            });
        });

        vm.moveto = function () {
            var movetoParames = {
                userLogonName: vm.clientProcess.currentActivity.approverLogonName,
                processID: vm.clientProcess.processID,
                resourceID: vm.clientProcess.resourceID,
                comment: vm.comment == ''? '同意': vm.comment
            };
            mcsWorkflowService.moveto(movetoParames, function (result) {
                alert("完成");
                $state.reload();
            });
        };

        vm.cancel = function () {
            var cancelParames = {
                userLogonName: vm.clientProcess.currentActivity.approverLogonName,
                processID: vm.clientProcess.processID,
                resourceID: vm.clientProcess.resourceID,
                comment: vm.comment == '' ? '拒绝' : vm.comment
            };
            mcsWorkflowService.cancel(cancelParames, function (result) {
                alert("完成");
                $state.reload();
            });
        };
    }])
})();