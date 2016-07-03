define(['angular', ppts.config.modules.query], function (ng, query) {
    query.registerFactory('studentQueryService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/query/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false },
                'get': { method: 'GET', isArray: true }
            });

        resource.queryCustomer = function (criteria, success, error) {
            resource.post({ operation: 'QueryCustomer' }, criteria, success, error);
        }

        return resource;
    }]);

    query.registerValue('studentListDataHeader', {
        /*
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
        */
    });
});