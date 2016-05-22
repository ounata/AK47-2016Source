define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('teacherCourseDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/teachercourse/:operation/:id',
        { operation: '@operation', id: '@id' },
        {
            'post': { method: 'POST' },
            'query': { method: 'GET', isArray: false }
        });

        resource.getStuCourse = function (criteria, success, error) {
            resource.post({ operation: 'getStuCourse' }, criteria, success, error);
        }

        return resource;
    }]);
});
