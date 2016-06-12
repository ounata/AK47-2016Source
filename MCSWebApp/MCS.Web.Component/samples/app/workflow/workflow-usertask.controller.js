(function() {
    angular.module('app.workflow')

    .controller('WorkflowUsertaskController', ['$scope', 'mcsWorkflowService', function ($scope, mcsWorkflowService) {
        var vm = this;

        vm.searchParams = {
            userLogonName: 'zhangxiaoyan_2'
        };

        vm.data = {
            selection: 'checkbox',
            rowsSelected: [],
            keyFields: ['taskGuid'],
            orderBy: [{
                dataField: 'taskStartTime',
                sortDirection: 1
            }],
            headers: [{
                    field: "taskTitle",
                    name: "标题",
                    headerCss: 'datatable-header-align-left',
                    sortable: false,
                    description: '标题',
                    template: '<a href="{{row.url}}">{{row.taskTitle}}</a>'
                },

                {
                    field: "sendToUserName",
                    name: "审批人",
                    headerCss: 'datatable-header-align-center',
                    sortable: false,
                    description: '审批人'
                },

                {
                    field: "body",
                    name: "节点名称",
                    headerCss: 'datatable-header-align-center',
                    sortable: false,
                    description: '节点名称'
                },

                {
                    field: "taskStartTime",
                    name: "开始时间",
                    headerCss: 'datatable-header-align-center',
                    sortable: false,
                    description: '开始时间',
                    template: '<span>{{row.taskStartTime | date: "yyyy-MM-dd HH:mm"}}</span>'
                }
            ],
            rows: [],
            pager: {
                noPager: true
            }
        };

        vm.search = function () {
            mcsWorkflowService.queryUsertask(vm.searchParams, function (result) {
                vm.data.rows = result.data;
            });
        };
    }])
})();