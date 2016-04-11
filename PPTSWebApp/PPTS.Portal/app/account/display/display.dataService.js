define([ppts.config.modules.account], function (account) {

    customer.registerFactory('accountDisplayDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/account/:id',
            {   id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        
        resource.GetAccountCombinationInfo = function (id, success, error) {
            resource.query({ id: id }, success, error);
        }

        return resource;
    }]);
});