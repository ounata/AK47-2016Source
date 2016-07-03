query.registerFactory('taskQueryService', ['$resource', function ($resource) {

    var resource = $resource(ppts.config.customerApiBaseUrl + 'api/query/:operation/:id',
        { operation: '@operation', id: '@id' },
        {
            'post': { method: 'POST' },
            'query': { method: 'GET', isArray: false },
            'get': { method: 'GET', isArray: true }
        });

    resource.queryTask = function (criteria, success, error) {
        resource.post({ operation: 'QueryTask' }, criteria, success, error);
    }

    return resource;
}]);