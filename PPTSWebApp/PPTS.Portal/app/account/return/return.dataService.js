define([ppts.config.modules.account], function (account) {

    account.registerFactory('accountReturnDataService', ['$resource', function ($resource) {

        var resource = $resource(ppts.config.customerApiBaseUrl + 'api/accounts/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        //根据学员ID获取服务费返还信息
        resource.getReturnApplyByCustomerID = function (id, success, error) {
            resource.query({ operation: 'GetReturnApplyByCustomerID', id: id }, success, error);
        }

        //保存服务费返还信息
        resource.saveReturnApply = function (apply, success, error) {
            resource.post({ operation: 'SaveReturnApply' }, apply, success, error);
        }

        return resource;
    }]);
});