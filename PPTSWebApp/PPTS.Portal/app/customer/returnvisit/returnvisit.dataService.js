define([ppts.config.modules.customer, function (customer) {

    customer.registerFactory('customerDataService', ['$resource', 'dataService', function ($resource, dataService) {

        var resource = $resource(ppts.config.customerApiServerPath + 'api/potentialcustomers/:operation/:id', { operation: '@operation', id: '@id' }, { 'post': { method: 'POST' } });

        resource.getAllCustomers = function (criteria, success, error) {
            dataService.process(resource.post({ operation: 'getAllCustomers' }, criteria).then(success, error, notify));
        }

        resource.getPagedCustomers = function (criteria, success, error, notify) {
            dataService.process(resource.post({ operation: 'getPagedCustomers' }, criteria).then(success, error, notify));
        }

        resource.getCustomer = function (customerId) {
            dataService.process(resource.query({ operation: 'getCustomerBaseinfo', id: customerId }).then(function (customer) {

            }));
        };

        resource.addCustomer = function () {
            dataService.process(resource.query({ operation: 'createCustomer' }).then(function (customer) {

            }));
        }

        resource.editCustomer = function (customerId) {
            dataService.process(resource.query({ operation: 'updateCustomer', id: customerId }).then(function (customer) {

            }));
        }

        resource.saveCustomer = function (customer, customerId) {
            if (!customerId) {
                dataService.process(resource.save({ operation: 'createCustomer' }, customer).then(function (result) {

                }));
            } else {
                dataService.process(resource.save({ operation: 'updateCustomer', id: customerId }, customer).then(function (result) {

                }));
            }
        };

        return resource;
    }]);
});