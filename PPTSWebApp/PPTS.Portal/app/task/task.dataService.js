define(['angular', ppts.config.modules.task], function (ng, task) {
    task.registerValue('userTaskListDataHeader', {
        headers: [{
            field: "taskTitle",
            sort: 'TASK_TITLE',
            name: "标题",
            template: '<a href="{{row.normalizedUrl}}" uib-popover="{{row.taskTitle|tooltip:20}}" popover-trigger="mouseenter">{{row.taskTitle|truncate:20}}</a>',
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

    task.registerValue('completedTaskListDataHeader', {
        headers: [{
            field: "taskTitle",
            sort: 'TASK_TITLE',
            name: "标题",
            template: '<a href="{{row.normalizedUrl}}" uib-popover="{{row.taskTitle|tooltip:50}}" popover-trigger="mouseenter">{{row.taskTitle|truncate:50}}</a>',
            sortable: true
        }, {
            field: "sourceName",
            sort: 'SOURCE_NAME',
            name: "发送人",
            sortable: true
        }, {
            field: "sendToUserName",
            name: "处理人"
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
            field: "completedTime",
            sort: 'COMPLETED_TIME',
            name: "任务完成时间",
            template: '<span>{{row.completedTime|date:"yyyy-MM-dd HH:mm:ss"|normalize}}</span>',
            sortable: true
        }, {
            field: "status",
            name: "处理状态",
            template: '<span>{{row.status|processStatus}}</span>'
        }],
        orderBy: [{ dataField: 'COMPLETED_TIME', sortDirection: 1 }]
    });

    task.registerValue('notifyListDataHeader', {
        headers: [{
            field: "taskTitle",
            sort: 'TASK_TITLE',
            name: "标题",
            template: '<a href="{{row.normalizedUrl}}" uib-popover="{{row.taskTitle|tooltip:20}}" popover-trigger="mouseenter">{{row.taskTitle|truncate:20}}</a>',
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
});