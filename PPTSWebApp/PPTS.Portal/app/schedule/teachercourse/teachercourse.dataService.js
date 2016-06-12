define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('teacherCourseDataService', ['$resource', 'autoCompleteDataService', function ($resource, autoCompleteDataService) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/teachercourse/:operation/:id',
        { operation: '@operation', id: '@id' },
        {
            'post': { method: 'POST' },
            'query': { method: 'GET', isArray: false }
        });

        resource.getTeacher = function (criteria, success, error) {
            //return $http.post(ppts.config.orderApiBaseUrl + 'api/teachercourse/getTeacher', criteria).then(success, error);
            return autoCompleteDataService.query(ppts.config.orderApiBaseUrl + 'api/teachercourse/getTeacher', criteria, success, error);
        }

        resource.getCurrUserCampus = function (success, error) {
            resource.post({ operation: 'getCurrUserCampus' }, success, error);
        }

        resource.initAccompanion = function (success, error) {
            resource.post({ operation: 'initAccompanion' }, success, error);
        }

        resource.addAccompanion = function (criteria, success, error) {
            resource.post({ operation: 'addAccompanion' }, criteria, success, error);
        }

        resource.cancelAssign = function (criteria, success, error) {
            resource.post({ operation: 'cancelAssign' }, criteria, success, error);
        }

        resource.deleteAssign = function (criteria, success, error) {
            resource.post({ operation: 'deleteAssign' }, criteria, success, error);
        }

        resource.getTchClassRecord = function (criteria, success, error) {
            resource.post({ operation: 'getTchClassRecord' }, criteria, success, error);
        }

        resource.getTchClassRecordPaged = function (criteria, success, error) {
            resource.post({ operation: 'getTchClassRecordPaged' }, criteria, success, error);
        }

        resource.markupAssign = function (criteria, success, error) {
            resource.post({ operation: 'markupAssign' }, criteria, success, error);
        }

        resource.getWeekCourse = function (criteria, success, error) {
            resource.post({ operation: 'getWeekCourse' }, criteria, success, error);
        }

        resource.initTchWeekCourse = function (criteria, success, error) {
            resource.post({ operation: 'initTchWeekCourse' }, criteria, success, error);
        }


        return resource;
    }]);
});
