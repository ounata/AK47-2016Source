define([], function () {
    var states = {
        //'ppts.product': {
        //    url: '/product',
        //    templateUrl: 'app/product/productlist/product-list/product-list.html',
        //    controller: 'productListController',
        //    dependencies: ['app/product/productlist/product-list/product-list.controller']
        //},
        'ppts.productAdd': {
            url: '/product/add/:type',
            templateUrl: 'app/product/productlist/product-add/product-add.html',
            controller: 'productAddController',
            dependencies: ['app/product/productlist/product-add/product-add.controller']
        },
        'ppts.productEdit': {
            url: '/product/edit/:id',
            templateUrl: 'app/product/productlist/product-edit/product-edit.html',
            controller: 'productEditController',
            dependencies: ['app/product/productlist/product-edit/product-edit.controller']
        },
        'ppts.productView': {
            url: '/product/view/:id',
            templateUrl: 'app/product/productlist/product-view/product-view.html',
            controller: 'productViewController',
            dependencies: ['app/product/productlist/product-view/product-view.controller']
        },
        'ppts.productCopy': {
            url: '/product/copy/:id',
            templateUrl: 'app/product/productlist/product-copy/product-copy.html',
            controller: 'productCopyController',
            dependencies: ['app/product/productlist/product-copy/product-copy.controller']
        },

    };

    return states;
});
