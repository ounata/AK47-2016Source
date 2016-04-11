define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('studentassignmentDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/studentassignment/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        
        resource.getAllStuUnAsgmt = function (criteria,success, error) {
            resource.post({ operation: 'getAllStudentAssignment' },criteria,success, error);
        }

        resource.getPagedStuUnAsgmt = function (criteria, success, error) {
            resource.post({ operation: 'getPagedStudentAssignment' }, criteria, success, error);
        }

        return resource;
    }]);
});
