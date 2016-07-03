query.registerFactory('accountQueryService', ['$resource', function ($resource) {

    var resource = $resource(ppts.config.customerApiBaseUrl + 'api/query/:operation/:id',
        { operation: '@operation', id: '@id' },
        {
            'post': { method: 'POST' },
            'query': { method: 'GET', isArray: false },
            'get': { method: 'GET', isArray: true }
        });

    resource.queryAccountCharge = function (criteria, success, error) {
        resource.post({ operation: 'QueryAccountCharge' }, criteria, success, error);
    }

    resource.queryAccountChargePayment = function (criteria, success, error) {
        resource.post({ operation: 'QueryAccountChargePayment' }, criteria, success, error);
    }

    resource.queryPosRecord = function (criteria, success, error) {
        resource.post({ operation: 'QueryPosRecord' }, criteria, success, error);
    }

    resource.queryAccountCharge = function (criteria, success, error) {
        resource.post({ operation: 'QueryAccountCharge' }, criteria, success, error);
    }

    return resource;
}]);