define(['angular'], function (ng) {
    var infra = ng.module('ppts.infra', []);

    // 配置provider
    infra.config(function ($controllerProvider, $compileProvider, $filterProvider, $provide) {
        mcs.util.configProvider(infra, $controllerProvider, $compileProvider, $filterProvider, $provide);
    });

    infra.config(function ($stateProvider) {
        mcs.util.loadRoute($stateProvider, {
            name: 'ppts.extragift',
            url: '/present/list',
            templateUrl: 'app/infra/extragift/present/present-list/present-list.html',
            controller: 'presentListController',
            breadcrumb: {
                label: '买赠表管理列表查询',
                parent: 'ppts'
            },
            dependencies: ['app/infra/extragift/present/present-list/present-list.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.presentAdd',
            url: '/present/add',
            templateUrl: 'app/infra/extragift/present/present-add/present-add.html',
            controller: 'presentAddController',            
            dependencies: ['app/infra/extragift/present/present-add/present-add.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.presentEdit',
            url: '/present/edit',
            templateUrl: 'app/infra/extragift/present/present-edit/present-edit.html',
            controller: 'presentEditController',            
            dependencies: ['app/infra/extragift/present/present-edit/present-edit.controller']
        }).loadRoute($stateProvider, {
            name: 'ppts.presentDetial',
            url: '/present/detial',
            templateUrl: 'app/infra/extragift/present/present-detial/present-detial.html',
            controller: 'presentDetialController',
            dependencies: ['app/infra/extragift/present/present-detial/present-detial.controller']
        });
    });

    return infra;
});