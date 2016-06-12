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
            breadcrumb: {
                label: '订购列表',
                parent: 'ppts'
            },
            dependencies: ['app/order/purchase/list/order-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseEditpayment',
            url: '/order/purchase/edit-payment/:orderid',
            templateUrl: 'app/order/purchase/list/edit-payment.html',
            controller: 'orderEditChargePayController',
            breadcrumb: {
                label: '编辑关联缴费单',
                parent: 'ppts.purchase'
            },
            dependencies: ['app/order/purchase/list/edit-payment.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchasePrint',
            url: '/order/purchase/print/:id',
            templateUrl: 'app/order/purchase/list/order-print.html',
            controller: 'orderPrintController',
            breadcrumb: {
                label: '订单打印',
                parent: 'ppts.purchase'
            },
            dependencies: ['app/order/purchase/list/order-print.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseHistory',
            url: '/order/purchase/history/:stuCode',
            templateUrl: 'app/order/purchase/history/order-history.html',
            controller: 'orderHistoryController',
            breadcrumb: {
                label: '订购历史',
                parent: 'ppts'
            },
            dependencies: ['app/order/purchase/history/order-history.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseExchange',
            url: '/order/purchase/history/exchange/:itemid/:type',
            templateUrl: 'app/order/purchase/history/exchange.html',
            controller: 'orderExchangeController',
            breadcrumb: {
                label: '资产兑换',
                parent: 'ppts.purchaseHistory'
            },
            dependencies: ['app/order/purchase/history/exchange.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseExchangeAmount',
            url: '/order/purchase/history/exchange-amount/:itemid/:type/:productid',
            templateUrl: 'app/order/purchase/history/exchange-amount.html',
            controller: 'orderExchangeAmountController',
            breadcrumb: {
                label: '资产兑换',
                parent: 'ppts.purchaseHistory'
            },
            dependencies: ['app/order/purchase/history/exchange-amount.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseOrderView',
            url: '/order/purchase/view/:orderId',
            templateUrl: 'app/order/purchase/view/order-view.html',
            controller: 'orderViewController',
            breadcrumb: {
                label: '查看订单',
                parent: 'ppts.purchase'
            },
            dependencies: ['app/order/purchase/view/order-view.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.unsubscribeProduct',
            url: '/order/purchase/list/unsubscribe/:id',
            templateUrl: 'app/order/purchase/list/unsubscribe.html',
            controller: 'orderListUnsubscribeController',
            breadcrumb: {
                label: '退订订单',
                parent: 'ppts.purchase'
            },
            dependencies: ['app/order/purchase/list/unsubscribe.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseProduct',
            url: '/order/purchase/product/all/:type/:customerId/:grade/:campusId',
            templateUrl: 'app/order/purchase/product/product-order.html',
            controller: 'orderProductController',
            breadcrumb: {
                label: '订购产品',
                parent: 'ppts'
            },
            dependencies: ['app/order/purchase/product/product-order.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseClassGroupList',
            url: '/order/purchase/product/classgrouplist/:customerId/:campusId',
            templateUrl: 'app/order/purchase/product/product-orderclassgrouplist.html',
            controller: 'orderClassGroupListController',
            breadcrumb: {
                label: '订购产品',
                parent: 'ppts'
            },
            dependencies: ['app/order/purchase/product/product-orderclassgrouplist.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseClassGroup',
            url: '/order/purchase/product/classgroup/:customerId/:listType/:productId',
            templateUrl: 'app/order/purchase/product/product-orderclassgroup.html',
            controller: 'orderClassGroupController',
            breadcrumb: {
                label: '订购产品',
                parent: 'ppts'
            },
            dependencies: ['app/order/purchase/product/product-orderclassgroup.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.purchaseOrderList',
            /*listType-[1:常规订购清单 2:买赠订购清单 3:插班订购清单]*/
            url: '/order/purchase/productlist/:listType/:customerId/:campusId',
            templateUrl: 'app/order/purchase/product/product-list.html',
            controller: 'orderListController',
            breadcrumb: {
                label: '订购清单',
                parent: 'ppts'
            },
            dependencies: ['app/order/purchase/product/product-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.unsubscribe',
            url: '/order/unsubscribe/list',
            templateUrl: 'app/order/unsubscribe/list/list.html',
            controller: 'unsubscribeListController',
            breadcrumb: {
                label: '退订管理',
                parent: 'ppts'
            },
            dependencies: ['app/order/unsubscribe/list/list.controller']
        })

    });



    return order;
});