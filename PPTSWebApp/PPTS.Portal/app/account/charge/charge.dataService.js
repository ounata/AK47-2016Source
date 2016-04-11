define([ppts.config.modules.account], function (account) {

    customer.registerFactory('accountChargeDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/account/charge/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        return resource;
    }]);
});