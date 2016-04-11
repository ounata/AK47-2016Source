define([ppts.config.modules.customer], function (customer) {

    customer.registerFactory('customerDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/potentialcustomers/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.getAllCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomers' }, criteria, success, error);
        }

        resource.getPagedCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getPagedCustomers' }, criteria, success, error);
        }

        resource.getCustomerForCreate = function (success, error) {
            resource.query({ operation: 'createCustomer' }, success, error);
        }

        resource.getCustomerForUpdate = function (id, success, error) {
            resource.query({ operation: 'updateCustomer', id: id }, success, error);
        }

        resource.createCustomer = function (model, success, error) {
            resource.save({ operation: 'createCustomer' }, model, success, error);
        };

        resource.updateCustomer = function (model, success, error) {
            resource.save({ operation: 'updateCustomer' }, model, success, error);
        };

        resource.getCustomerInfo = function (code, success, error) {
            resource.post({ operation: 'getCustomerInfo' }, { customerCode: code }, success, error);
        };

        resource.getStaffRelations = function (criteria, success, error) {
            resource.post({ operation: 'getStaffRelations' }, criteria, success, error);
        };

        resource.getPagedStaffRelations = function (criteria, success, error) {
            resource.post({ operation: 'getPagedStaffRelations' }, criteria, success, error);
        }

        return resource;
    }]);
});