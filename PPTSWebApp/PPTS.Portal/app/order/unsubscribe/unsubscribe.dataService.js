define([ppts.config.modules.order], function (customer) {

    customer.registerFactory('unsubscribeCourseDataService', ['$resource', function ($resource, dataService) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/unsubscribe/:operation/:id',
            { operation: '@operation' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });
        //--------------------------分页--------------------------------------------
        resource.getAllDebookOrders = function (criteria, callback) {
            resource.post({ operation: 'GetAllDebookOrders' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }

        resource.getPagedDebookOrders = function (criteria, callback) {
            resource.post({ operation: 'GetPagedDebookOrders' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        //--------------------------分页--------------------------------------------



        return resource;
    }]);
});