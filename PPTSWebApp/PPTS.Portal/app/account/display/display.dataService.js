define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountDisplayDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        
        //获取账户列表
        resource.getAccountList = function (customerID, success, error) {
            resource.query({ operation: 'GetAccountList',customerID:customerID }, success, error);
        }

        return resource;
    }]);
});