define([ppts.config.modules.order], function (order) {

    order.registerFactory('purchaseCourseDataService', ['$resource', function ($resource, dataService) {

        var resource = $resource(ppts.config.orderApiBaseUrl + 'api/purchase/:operation/:id',
            { operation: '@operation' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });


        //-----------------------订购单列表--------------------------
        resource.getAllOrderItems = function (criteria, callback) {
            resource.post({ operation: 'GetAllOrderItems' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        resource.getPagedOrderItems = function (criteria, callback) {
            resource.post({ operation: 'GetPagedProducts' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        //---------------------------------------------------------


        //-----------------------常规订购--------------------------
        //使用 product.service
        //---------------------------------------------------------


        //------------------------班组订购-------------------------
        resource.getClassGroupProductList = function (criteria, callback) {
            resource.post({ operation: 'GetClassGroupProductList' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }
        resource.getPagedClassGroupProducts = function (criteria, callback) {
            resource.post({ operation: 'GetPagedClassGroupProducts' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }

        resource.getPagedClasses = function (criteria, callback) {
            resource.post({ operation: 'GetPagedClasses' }, criteria, function (entity) { if (callback) { callback(entity); } });
        }

        //---------------------------------------------------------


        resource.getShoppingCart = function (data, callback) {
            resource.post({ operation: 'GetShoppingCart' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        resource.deleteShoppingCart = function (cartIDs, callback) {
            resource.post({ operation: 'DeleteShoppingCart' }, cartIDs, function (entity) { if (callback) { callback(entity); } });
        }

        resource.addShoppingCart = function (data, callback) {
            resource.post({ operation: 'AddShoppingCart' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        resource.submitShoppingCart = function (data, callback) {
            resource.post({ operation: 'SubmitShoppingCart' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        //获取要扣取的服务费
        resource.getServiceMoney = function (customerId, callback) {
            //resource.post({ operation: 'SubmitShoppingCart' }, data, function (entity) { if (callback) { callback(entity); } });
        }

        return resource;
    }]);

});