(function() {
    'use strict';

    mcs.ng.service('mcsWorkflowService', ['$resource', function($resource) {
        var service = this;

        var resource = $resource(ppts.config.workflowApiBaseUrl + 'api/workflow/:operation/:id', {
            operation: '@operation',
            id: '@id'
        }, {
            'post': {
                method: 'POST'
            },
            'polling': {
                method: 'POST',
                headers: {
                    'pollingTasks': true
                }
            },
            'query': {
                method: 'GET',
                isArray: false
            }
        });

        service.goBack = function() {
            history.back();
        };

        service.startup = function(startupParames, success, error) {
            resource.post({
                operation: 'Startup'
            }, JSON.stringify(startupParames), success, error);
        };
        //service.queryUsertask = function (searchParams, success, error) {
        //    resource.post({ operation: 'QueryUsertask' }, JSON.stringify(searchParams), success, error);
        //};
        service.startupFreeSteps = function (startupFreeStepsParams, success, error) {
            resource.post({
                operation: 'StartupFreeSteps'
            }, JSON.stringify(startupFreeStepsParams), success, error);
        };
        service.getClientProcess = function(searchParams, success, error) {
            resource.post({
                operation: 'GetClientProcess'
            }, JSON.stringify(searchParams), success, error);
        };
        service.moveto = function(movetoParames, success, error) {
            resource.post({
                operation: 'Moveto'
            }, JSON.stringify(movetoParames), success, error);
        };
        service.cancel = function(cancelParames, success, error) {
            resource.post({
                operation: 'Cancel'
            }, JSON.stringify(cancelParames), success, error);
        };
        service.save = function (saveParames, success, error) {
            resource.post({
                operation: 'Save'
            }, JSON.stringify(saveParames), success, error);
        };
        service.withdraw = function (withdrawParames, success, error) {
            resource.post({
                operation: 'Withdraw'
            }, JSON.stringify(withdrawParames), success, error);
        };
        service.getForm = function (searchParams, success, error) {
            resource.post({
                operation: 'GetForm'
            }, JSON.stringify(searchParams), success, error);
        };
        return service;
    }]);
})();
