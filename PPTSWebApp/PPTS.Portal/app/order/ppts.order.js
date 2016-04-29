define(['angular'], function (ng) {
    var order = ng.module('ppts.order', []);

    // 配置provider
    order.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(order, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    // 配置路由
    order.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.purchase',
            url: '/order/purchase',
            templateUrl: 'app/order/purchase/list/order-list.html',
            controller: 'orderListController',
            dependencies: ['app/order/purchase/list/order-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseProduct',
            url: '/order/purchase/product/1',
            templateUrl: 'app/order/purchase/product/product-order.html',
            controller: 'orderProductController',
            dependencies: ['app/order/purchase/product/product-order.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseClassGroupList',
            url: '/order/purchase/product/2',
            templateUrl: 'app/order/purchase/product/product-orderclassgrouplist.html',
            controller: 'orderClassGroupListController',
            dependencies: ['app/order/purchase/product/product-orderclassgrouplist.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseClassGroup',
            url: '/order/purchase/product/2/:productId',
            templateUrl: 'app/order/purchase/product/product-orderclassgroup.html',
            controller: 'orderClassGroupController',
            dependencies: ['app/order/purchase/product/product-orderclassgroup.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseOrderList',
            url: '/order/purchase/productlist/:listType',
            templateUrl: 'app/order/purchase/product/product-list.html',
            controller: 'orderListController',
            dependencies: ['app/order/purchase/product/product-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.unsubscribe',
            url: '/order/unsubscribe/list',
            templateUrl: 'app/order/unsubscribe/list/list.html',
            controller: 'unsubscribeListController',
            dependencies: ['app/order/unsubscribe/list/list.controller']
        })

    });



    return order;
});