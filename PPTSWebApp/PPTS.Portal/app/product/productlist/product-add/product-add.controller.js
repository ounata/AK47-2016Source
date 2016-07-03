define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService,
        ppts.config.dataServiceConfig.productEditHelper],
        function (helper) {
            helper.registerController('productAddController', [
                '$scope', '$location', '$state', '$stateParams', 'dataSyncService', 'productDataService', 'productEditHelper',
                function ($scope, $location, $state, $stateParams, dataSyncService, productDataService, productEditHelper) {

                    console.warn($location.$$search)

                    var vm = this;
                    var loadtype = $location.$$search.ltype;

                    productEditHelper.init($scope, vm, loadtype, productDataService, $state, function () {

                        //加载信息
                        productDataService.addProductView($scope.tabs[loadtype].index, function (entity) {
                            productEditHelper.callback($scope, vm, loadtype, entity);
                        });

                    });

                }]);
        });

