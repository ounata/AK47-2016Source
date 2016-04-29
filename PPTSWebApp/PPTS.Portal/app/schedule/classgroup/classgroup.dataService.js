define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('classgroupDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/classgroup/:operation/:id', { operation: '@operation', id: '@id' }, { 'post': { method: 'POST' } });

        resource.getAllClasses = function (criteria, success, error) {
            resource.post({ operation: 'getAllClasses' }, criteria, success, error);
        }

        resource.getPageClasses = function (criteria, success, error) {
            resource.post({ operation: 'getPageClasses' }, criteria, success, error);
        }

        resource.createClass = function (model, success, error) {
            resource.post({ operation: 'createClass' }, model, success, error);
        }

        resource.getAllAssets = function (criteria, success, error) {
            resource.post({ operation: 'getAllAssets' }, criteria, success, error);
        }

        resource.getPageAssets = function (criteria, success, error) {
            resource.post({ operation: 'getPageAssets' }, criteria, success, error);
        }

        resource.getAllTeacherJobs = function (criteria, success, error) {
            resource.post({ operation: 'getAllTeacherJobs' }, criteria, success, error);
        }

        resource.getPageTeacherJobs = function (criteria, success, error) {
            resource.post({ operation: 'getPageTeacherJobs' }, criteria, success, error);
        }
        

        return resource;
    }]);
});
