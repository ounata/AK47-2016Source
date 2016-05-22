define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('teacherAssignmentDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/teacherassignment/:operation/:id',
        { operation: '@operation', id: '@id' },
        {
            'post': { method: 'POST' },
            'query': { method: 'GET', isArray: false }
        });


        resource.getTeacherList = function (criteria, success, error) {
            resource.post({ operation: 'getTeacherList' }, criteria, success, error);
        }

        resource.getTeacherListPaged = function (criteria, success, error) {
            resource.post({ operation: 'getTeacherListPaged' }, criteria, success, error);
        }

        resource.getTeacherWeekCourse = function (criteria, success, error) {
            resource.post({ operation: 'getTeacherWeekCourse' }, criteria, success, error);
        }

        resource.getCurMonthStat = function (criteria, success, error) {
            resource.post({ operation: 'getCurMonthStat' }, criteria, success, error);
        }

        resource.initCreateAssign = function (criteria, success, error) {
            resource.post({ operation: 'initCreateAssign' }, criteria, success, error);
        }

        resource.getAssetByCustomerID = function (criteria, success, error) {
            resource.post({ operation: 'getAssetByCustomerID' }, criteria, success, error);
        }

        resource.createAssign = function (criteria, success, error) {
            resource.post({ operation: 'createAssign' }, criteria, success, error);
        }

        resource.getAssetByAssetID = function (criteria, success, error) {
            resource.post({ operation: 'getAssetByAssetID' }, criteria, success, error);
        }

        resource.cancelAssign = function (criteria, success, error) {
            resource.post({ operation: 'cancelAssign' }, criteria, success, error);
        }

        resource.copyAssign = function (criteria, success, error) {
            resource.post({ operation: 'copyAssign' }, criteria, success, error);
        }

        resource.initResetAssign = function (criteria, success, error) {
            resource.post({ operation: 'initResetAssign' }, criteria, success, error);
        }

        resource.resetAssign = function (criteria, success, error) {
            resource.post({ operation: 'resetAssign' }, criteria, success, error);
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
