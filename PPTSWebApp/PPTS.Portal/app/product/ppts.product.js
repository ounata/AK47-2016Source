define(['angular'], function (ng) {
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
            breadcrumb: {
                label: '产品列表',
                parent: 'ppts'
            },
            dependencies: ['app/product/productlist/product-list/product-list.controller']
            })
            .loadRoute($stateProvider, {
                name: 'ppts.productAdd',
                url: '/product/add',
                templateUrl: 'app/product/productlist/product-add/product-add.html',
                controller: 'productAddController',
                dependencies: ['app/product/productlist/product-add/product-add.controller']
            })
            .loadRoute($stateProvider, {
                name: 'ppts.productAdd.onetoone',
                url: '/onetoone?loadtype=:ltype',
                breadcrumb: {
                    label: '添加产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-onetoone.html',
            }).loadRoute($stateProvider, {
                name: 'ppts.productAdd.classgroup',
                url: '/classgroup?loadtype=:ltype',
                breadcrumb: {
                    label: '添加产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-classgroup.html',
            }).loadRoute($stateProvider, {
                name: 'ppts.productAdd.youxue',
                url: '/youxue?loadtype=:ltype',
                breadcrumb: {
                    label: '添加产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
            }).loadRoute($stateProvider, {
                name: 'ppts.productAdd.dailizhaosheng',
                url: '/dailizhaosheng?loadtype=:ltype',
                breadcrumb: {
                    label: '添加产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
            }).loadRoute($stateProvider, {
                name: 'ppts.productAdd.other',
                url: '/other?loadtype=:ltype',
                breadcrumb: {
                    label: '添加产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
            })

            .loadRoute($stateProvider, {
                name: 'ppts.productView',
                url: '/product/view/:id',
                templateUrl: 'app/product/productlist/product-view/product-view.html',
                controller: 'productViewController',
                breadcrumb: {
                    label: '查看产品',
                    parent: 'ppts.product'
                },
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
                url: '/onetoone/?:ltype&:id',
                breadcrumb: {
                    label: '复制产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-onetoone.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy.classgroup',
                url: '/classgroup/?:ltype&:id',
                breadcrumb: {
                    label: '复制产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-classgroup.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy.youxue',
                url: '/youxue/?:ltype&:id',
                breadcrumb: {
                    label: '复制产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy.dailizhaosheng',
                url: '/dailizhaosheng/?:ltype&:id',
                breadcrumb: {
                    label: '复制产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.productCopy.other',
                url: '/other/?:ltype&:id',
                breadcrumb: {
                    label: '复制产品',
                    parent: 'ppts.product'
                },
                templateUrl: 'app/product/productlist/product-add/product-add-other.html',
                controller: 'productCopyController',
            }).loadRoute($stateProvider, {
                name: 'ppts.classAdd',
                url: '/class/add/:id',
                templateUrl: 'app/schedule/classgroup/class-add/class-add.html',
                controller: 'classAddController',
                dependencies: [
                                'app/schedule/classgroup/class-add/class-add.controller',
                                'app/schedule/classgroup/class-add/dayofweek.controller',
                                'app/schedule/classgroup/customer-add/customer-add.controller',
                                'app/schedule/classgroup/teacher-add/teacher-add.controller'
                            ],
                breadcrumb: {
                    label: '新增班级',
                    parent: 'ppts'
                }
            }).loadRoute($stateProvider, {
                name: 'ppts.productApprove',
                url: '/product/approve?processID&activityID&resourceID',
                templateUrl: 'app/product/productlist/product-approve/product-approve.html',
                controller: 'productApproveController',
                breadcrumb: {
                    label: '产品审批',
                    parent: 'ppts.product'
                },
                dependencies: ['app/product/productlist/product-approve/product-approve.controller']
            });

        

    });




    return product;
});
