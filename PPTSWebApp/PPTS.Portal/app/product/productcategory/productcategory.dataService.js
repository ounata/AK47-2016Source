define(['product'], function (product) {

    product.registerFactory('productDataService', ['$resource', function ($resource) {

        var resource = $resource(mcs.app.config.productApiServerPath + 'api/products/:operation/:id', { operation: '@operation', id: '@id' }, { 'post': { method: 'POST' } });

        resource.getAllProducts = function (criteria, success, error) {
            resource.post({ operation: 'getAllProducts' }, criteria, success, error);
        }

        resource.getPagedProducts = function (criteria, success, error) {
            resource.post({ operation: 'getPagedProducts' }, criteria, success, error);
        }

        resource.getProduct = function (productId) {
            resource.query({ operation: 'getProductBaseinfo', id: productId }, function (product) {

            });
        };

        resource.addProduct = function () {
            resource.query({ operation: 'createProduct' }, function (product) {

            });
        }

        resource.editProduct = function (productId) {
            resource.query({ operation: 'updateProduct', id: productId }, function (product) {

            });
        }

        resource.saveProduct = function (product, productId) {
            if (!productId) {
                resource.save({ operation: 'createProduct' }, product, function (result) {

                });
            } else {
                resource.save({ operation: 'updateProduct', id: productId }, product, function (result) {

                });
            }
        };

        return resource;
    }]);
});
