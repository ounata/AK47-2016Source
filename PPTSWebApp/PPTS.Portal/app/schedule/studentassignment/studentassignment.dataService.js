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

        resource.getAssignCondition = function (criteria, success, error) {
            resource.post({ operation: 'getAssignCondition' }, criteria, success, error);
        }

        resource.createAssignCondition = function (criteria, success, error) {
            resource.post({ operation: 'createAssignCondition' }, criteria, success, error);
        }

        resource.getStudentWeekCourse = function (criteria, success, error) {
            resource.post({ operation: 'getStudentWeekCourse' }, criteria, success, error);
        }

        resource.cancelAssign = function (criteria, success, error) {
            resource.post({ operation: 'cancelAssign' }, criteria, success, error);
        }

        resource.copyAssign = function (criteria, success, error) {
            resource.post({ operation: 'copyAssign' }, criteria, success, error);
        }

        resource.resetAssignInit = function (success, error) {
            resource.post({ operation: 'resetAssignInit' },success, error);
        }

        resource.resetAssign = function (criteria, success, error) {
            resource.post({ operation: 'resetAssign' }, criteria, success, error);
        }

        resource.getCurMonthStat = function (criteria, success, error) {
            resource.post({ operation: 'getCurMonthStat' }, criteria, success, error);
        }

        resource.getSCLV = function (criteria, success, error) {
            resource.post({ operation: 'getSCLV' }, criteria, success, error);
        }

        resource.getPagedSCLV = function (criteria, success, error) {
            resource.post({ operation: 'getPagedSCLV' }, criteria, success, error);
        }

        

        return resource;
    }]);
});
