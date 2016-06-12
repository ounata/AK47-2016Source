define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService,
        ppts.config.dataServiceConfig.productEditHelper],
        function (helper) {
            helper.registerController('productCopyController', [
                '$scope', '$location', '$state', '$stateParams', 'dataSyncService', 'productDataService','productEditHelper',

                function ($scope, $location, $state, $stateParams, dataSyncService, productDataService, productEditHelper) {

                    var vm = this;
                    var id = $stateParams.id;
                    var loadtype = $location.$$search.ltype;

                    productEditHelper.init($scope, vm, loadtype, productDataService, function () {

                        //加载信息
                        productDataService.copyProductView(id, function (entity) {
                            productEditHelper.callback($scope, vm, loadtype, entity);
                        });

                    });

                    $scope.tag = '复制';

                }

            ]);
        });

