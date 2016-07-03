query.registerFactory('orderQueryService', ['$resource', function ($resource) {

    var resource = $resource(ppts.config.orderApiBaseUrl + 'api/query/:operation/:id',
        { operation: '@operation', id: '@id' },
        {
            'post': { method: 'POST' },
            'query': { method: 'GET', isArray: false },
            'get': { method: 'GET', isArray: true }
        });

    resource.queryOrder = function (criteria, success, error) {
        resource.post({ operation: 'QueryOrder' }, criteria, success, error);
    }

    resource.queryOrderExchange = function (criteria, success, error) {
        resource.post({ operation: 'QueryOrderExchange' }, criteria, success, error);
    }

    resource.queryOrderStock = function (criteria, success, error) {
        resource.post({ operation: 'queryOrderStock' }, criteria, success, error);
    }

    return resource;
}]);