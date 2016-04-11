﻿define(['angular'], function (ng) {
    var product = ng.module('ppts.product', []);

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
        })
            .loadRoute($stateProvider, {
                name: 'ppts.productAdd',
                url: '/product/add',
                templateUrl: 'app/product/productlist/product-add/product-add.html',
                controller: 'productAddController',
                dependencies: ['app/product/productlist/product-add/product-add.controller'],
                
            })
            .loadRoute($stateProvider, {
                name: 'ppts.productAdd.onetoone',
                url: '/onetoone',
                templateUrl: 'app/product/productlist/product-add/product-add-onetoone.html',
            }).loadRoute($stateProvider, {
                name: 'ppts.productAdd.classgroup',
                url: '/classgroup',
                templateUrl: 'app/product/productlist/product-add/product-add-classgroup.html',
            }).loadRoute($stateProvider, {
                name: 'ppts.productAdd.youxue',
                url: '/youxue',
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
            }).loadRoute($stateProvider, {
                name: 'ppts.productAdd.wukeshou',
                url: '/wukeshou',
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
            }).loadRoute($stateProvider, {
                name: 'ppts.productAdd.other',
                url: '/other',
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
            })

            .loadRoute($stateProvider, {
                name: 'ppts.productView',
                url: '/product/view/:id',
                templateUrl: 'app/product/productlist/product-view/product-view.html',
                controller: 'productViewController',
                dependencies: ['app/product/productlist/product-view/product-view.controller']
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy',
                url: '/product/copy',
                templateUrl: 'app/product/productlist/product-copy/product-copy.html',
                //controller: 'productCopyController',
                dependencies: ['app/product/productlist/product-copy/product-copy.controller']
            })
            .loadRoute($stateProvider, {
                name: 'ppts.productCopy.onetoone',
                url: '/onetoone/:id',
                templateUrl: 'app/product/productlist/product-add/product-add-onetoone.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy.classgroup',
                url: '/classgroup/:id',
                templateUrl: 'app/product/productlist/product-add/product-add-classgroup.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy.youxue',
                url: '/youxue/:id',
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy.wukeshou',
                url: '/wukeshou/:id',
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy.other',
                url: '/other/:id',
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
                controller: 'productCopyController',
            });

        


    });




    return product;
});
