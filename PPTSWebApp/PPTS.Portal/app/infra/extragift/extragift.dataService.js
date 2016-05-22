define([ppts.config.modules.infra], function (infra) {

    infra.registerFactory('extraGiftDataService', ['$resource', 'dataService', function ($resource, dataService) {

        var resource = $resource(ppts.config.productApiBaseUrl + 'api/extraGift/:operation/:id', { operation: '@operation', id: '@id' }, { 'post': { method: 'POST' } });
        


        return resource;
    }]);
});