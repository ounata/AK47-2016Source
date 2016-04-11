define([ppts.config.modules.account], function (account) {

    customer.registerFactory('accountTransferDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/account/transfer/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        return resource;
    }]);
});