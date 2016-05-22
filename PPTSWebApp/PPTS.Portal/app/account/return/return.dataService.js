define([ppts.config.modules.account], function (account) {

    customer.registerFactory('accountReturnDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        //获取服务费列表
        resource.getCustomerExpenses = function (id, success, error) {
            resource.query({ operation: 'GetCustomerExpenses', customerID: id }, success, error);
        }


        return resource;
    }]);
});