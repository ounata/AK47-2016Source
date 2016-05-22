define([ppts.config.modules.schedule], function (schedule) {

    schedule.registerFactory('classgroupDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/classgroup/:operation/:id', { operation: '@operation', id: '@id' }, { 'post': { method: 'POST' }, 'query': { method: 'GET', isArray: false } });

        resource.getAllClasses = function (criteria, success, error) {
            resource.post({ operation: 'getAllClasses' }, criteria, success, error);
        }

        resource.getPageClasses = function (criteria, success, error) {
            resource.post({ operation: 'getPageClasses' }, criteria, success, error);
        }

        resource.getCustomerAllClasses = function (criteria, success, error) {
            resource.post({ operation: 'getCustomerAllClasses' }, criteria, success, error);
        }

        resource.createClass = function (model, success, error) {
            resource.post({ operation: 'createClass' }, model, success, error);
        }

        resource.deleteClass = function (id, success, error) {
            var data = { classID: id };
            resource.post({ operation: 'deleteClass' }, data, success, error);
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
        
        resource.getAllCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomers' }, criteria, success, error);
        }

        resource.getPageCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getPageCustomers' }, criteria, success, error);
        }

        resource.deleteCustomer = function (model, success, error) {
            resource.post({ operation: 'deleteCustomer' }, model, success, error);
        }

        resource.getClassDetail = function (criteria, success, error) {
            resource.post({ operation: 'getClassDetail' }, criteria, success, error);
        }

        resource.getPageClassLessons = function (criteria, success, error) {
            resource.post({ operation: 'getPageClassLessons' }, criteria, success, error);
        }

        resource.editClassLessones = function (model, success, error) {
            resource.post({ operation: 'editClassLessones' }, model, success, error);
        }

        resource.editTeacher = function (model, success, error) {
            resource.post({ operation: 'editTeacher' }, model, success, error);
        }

        resource.addCustomer = function (model, success, error) {
            resource.post({ operation: 'addCustomer' }, model, success, error);
        }

        resource.checkCreateClass_Product = function (id, success, error) {
            var data = { productID: id };
            resource.post({ operation: 'checkCreateClass_Product' }, data, success, error);
        }

        return resource;
    }]);
});
