define([ppts.config.modules.customer], function (customer) {

    customer.registerFactory('followDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/customerfollows/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.getAllFollows = function (criteria, success, error) {
            resource.post({ operation: 'getAllFollows' }, criteria, success, error);
        }

        resource.getPagedFollows = function (criteria, success, error) {
            resource.post({ operation: 'getPagedFollows' }, criteria, success, error);
        }
        /*
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
        */
        return resource;
    }]);
});