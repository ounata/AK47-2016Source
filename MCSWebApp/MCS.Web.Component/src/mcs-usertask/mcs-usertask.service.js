(function () {
    'use strict';

    mcs.ng.service('mcsUserTaskService', ['$resource', function ($resource) {
        var service = this;

        var resource = $resource(ppts.config.mcsApiBaseUrl + 'api/usertask/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'polling': { method: 'POST', headers: { 'pollingTasks': true } },
                'query': { method: 'GET', isArray: false }
            });

        service.queryUserTasksAndCount = function (searchParams, success, error) {
            resource.polling({ operation: 'QueryUserTasksAndCount' }, JSON.stringify(searchParams), success, error);
        };

        service.queryUserTasks = function (searchParams, success, error) {
            if (!searchParams || !searchParams.dataType) return;
            var action = '';
            switch (searchParams.dataType) {
                case 'userTask':
                    action = 'QueryUserTasks';
                    break;
                case 'completedTask':
                    action = 'QueryUserCompletedTasks';
                    break;
                case 'notify':
                    action = 'QueryUserNotifies';
                    break;
            }
            resource.post({ operation: action }, JSON.stringify(searchParams), success, error);
        };
      
        return service;
    }]);
})();