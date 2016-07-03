define(['angular'], function (ng) {
    var task = ng.module('ppts.task', []);

    // 配置provider
    task.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(task, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    // 配置路由
    task.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.userTask',
            url: '/user-tasks',
            templateUrl: 'app/task/user-task/task-list.html',
            controller: 'userTaskListController',
            breadcrumb: {
                label: '待办列表',
                parent: 'ppts'
            },
            dependencies: ['app/task/user-task/task-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.completedTask',
            url: '/user-completed-tasks',
            templateUrl: 'app/task/completed-task/completed-task-list.html',
            controller: 'completedTaskListController',
            breadcrumb: {
                label: '已办列表',
                parent: 'ppts'
            },
            dependencies: ['app/task/completed-task/completed-task-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.notify',
            url: '/notify',
            templateUrl: 'app/task/notify/notify-list.html',
            controller: 'notifyListController',
            breadcrumb: {
                label: '通知列表',
                parent: 'ppts'
            },
            dependencies: ['app/task/notify/notify-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.workflow',
            url: '/workflow',
            templateUrl: 'app/task/workflow/workflow.html',
            controller: 'workflowController',
            breadcrumb: {
                label: '工作流启动表单',
                parent: 'ppts'
            },
            dependencies: ['app/task/workflow/workflow.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.workflow-approve',
            url: '/workflow/approve?processID&activityID&resourceID',
            templateUrl: 'app/task/workflow/workflow-approve.html',
            controller: 'workflowApproveController',
            breadcrumb: {
                label: '工作流审批表单',
                parent: 'ppts'
            },
            dependencies: ['app/task/workflow/workflow-approve.controller']
        });
    });

    return task;
});