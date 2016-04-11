define([ppts.config.modules.product], function (product) {

    product.registerFactory('productDataService', ['$resource', function ($resource) {




        var resource = $resource(ppts.config.productApiBaseUrl + 'api/products/:operation/:id',
            { operation: '@operation' },
            {
                'post': { method: 'POST' },
                'query': { method: 'GET', isArray: false }
            });

        resource.addProductView = function (type,callback) {
            resource.query({ operation: 'createProduct', type:type }, function (product) { if (callback) { callback(product); } });
        }

        resource.copyProductView = function (id, callback) {
            resource.query({ operation: 'copyProduct', id: id }, function (product) { if (callback) { callback(product); } });
        }

        //resource.saveProduct = function (vmodel, callback) {
        //    console.log(vmodel)
        //    resource.post({ operation: 'saveProduct' }, vmodel, function (product) { if (callback) { callback(product); } });
        //}

        resource.delProduct = function (ids, callback) {
            resource.post({ operation: 'delProduct' }, ids, function (entity) { if (callback) { callback(entity); } });
        }

        resource.stopProduct = function (id, callback) {
            resource.query({ operation: 'stopSellProduct', id: id }, function (entity) { if (callback) { callback(entity); } });
        }

        resource.startProduct = function (id, callback) {
            resource.post({ operation: 'startSellProduct' }, id, function (entity) { if (callback) { callback(entity); } });
        }

        resource.submitProduct = function (vmodel, callback) {
            resource.post({ operation: 'submitProduct' }, vmodel, function (product) { if (callback) { callback(product); } });
        }


        resource.getProduct = function (id, callback) {
            resource.query({ operation: 'getProduct', id: id }, function (product) { if (callback) { callback(product); } });
        }



        resource.getAllProducts = function (criteria, success, error) {
            resource.post({ operation: 'getAllProducts' }, criteria, success, error);
        }

        resource.getPagedProducts = function (criteria, success, error) {
            resource.post({ operation: 'getPagedProducts' }, criteria, success, error);
        }




        //resource.getProduct = function (productId) {
        //    resource.query({ operation: 'getProductBaseinfo', id: productId }, function (product) {

        //    });
        //};



        //resource.editProduct = function (productId) {
        //    resource.query({ operation: 'updateProduct', id: productId }, function (product) {

        //    });
        //}

        //resource.saveProduct = function (product, productId) {
        //    if (!productId) {
        //        resource.save({ operation: 'createProduct' }, product, function (result) {

        //        });
        //    } else {
        //        resource.save({ operation: 'updateProduct', id: productId }, product, function (result) {

        //        });
        //    }
        //};

        return resource;
    }]);
});
