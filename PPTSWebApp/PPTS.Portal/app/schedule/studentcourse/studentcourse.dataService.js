define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('studentCourseDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/studentcourse/:operation/:id',
           { operation: '@operation', id: '@id' },
           {
               'post': { method: 'POST' },
               'query': { method: 'GET', isArray: false }
           });

        resource.getStuCourse = function (criteria, success, error) {
            resource.post({ operation: 'getStuCourse' }, criteria, success, error);
        }

        resource.getStuCoursePaged = function (criteria, success, error) {
            resource.post({ operation: 'getStuCoursePaged' }, criteria, success, error);
        }

        resource.deleteAssign = function (criteria, success, error) {
            resource.post({ operation: 'deleteAssign' }, criteria, success, error);
        }

        resource.confirmAssign = function (criteria, success, error) {
            resource.post({ operation: 'confirmAssign' }, criteria, success, error);
        }

        resource.markupAssign = function (criteria, success, error) {
            resource.post({ operation: 'markupAssign' }, criteria, success, error);
        }

        return resource;
    }]);
});
