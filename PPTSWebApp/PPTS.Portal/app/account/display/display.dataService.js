define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountDisplayDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        
        //获取账户查询的信息
        resource.getAccountQueryResult = function (customerID, success, error) {
            resource.query({ operation: 'GetAccountQueryResult',customerID:customerID }, success, error);
        }

        return resource;
    }]);
});