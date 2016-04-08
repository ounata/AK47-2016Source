define(['angular'], function (ng) {
    var product= ng.module('ppts.product', []);

    // 配置provider
    product.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(product, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });
    // 配置路由
    product.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.product',
            url: '/product',
            templateUrl: 'app/product/productlist/product-list/product-list.html',
            controller: 'productListController',
            dependencies: ['app/product/productlist/product-list/product-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.productAdd',
            url: '/product/add/:type',
            templateUrl: 'app/product/productlist/product-add/product-add.html',
            controller: 'productAddController',
            dependencies: ['app/product/productlist/product-add/product-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.productEdit',
            url: '/product/edit/:id',
            templateUrl: 'app/product/productlist/product-edit/product-edit.html',
            controller: 'productEditController',
            dependencies: ['app/product/productlist/product-edit/product-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.productView',
            url: '/product/view/:id',
            templateUrl: 'app/product/productlist/product-view/product-view.html',
            controller: 'productViewController',
            dependencies: ['app/product/productlist/product-view/product-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.productCopy',
            url: '/product/copy/:id',
            templateUrl: 'app/product/productlist/product-copy/product-copy.html',
            controller: 'productCopyController',
            dependencies: ['app/product/productlist/product-copy/product-copy.controller']
        });
    });

    return product;
});
