(function () {
    'use strict';

    mcs.ng.service('mcsWorkflowService', ['$resource', function ($resource) {
        var service = this;

        var resource = $resource(ppts.config.mcsApiBaseUrl + '/api/workflow/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        service.startup = function (startupParames, success, error) {
            resource.post({ operation: 'Startup' }, JSON.stringify(startupParames), success, error);
        };
        service.queryUsertask = function (searchParams, success, error) {
            resource.post({ operation: 'QueryUsertask' }, JSON.stringify(searchParams), success, error);
        };
        service.getClientProcess = function (searchParams, success, error) {
            resource.post({ operation: 'GetClientProcess' }, JSON.stringify(searchParams), success, error);
        };
        service.moveto = function (movetoParames, success, error) {
            resource.post({ operation: 'Moveto' }, JSON.stringify(movetoParames), success, error);
        };
        service.cancel = function (cancelParames, success, error) {
            resource.post({ operation: 'Cancel' }, JSON.stringify(cancelParames), success, error);
        };

        return service;
    }]);
})();