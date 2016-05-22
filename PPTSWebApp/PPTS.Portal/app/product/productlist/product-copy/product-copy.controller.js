define([ppts.config.modules.product,
        ppts.config.dataServiceConfig.productDataService,
        ppts.config.dataServiceConfig.productEditHelper],
        function (helper) {
            helper.registerController('productCopyController', [
                '$scope', '$location', '$state', '$stateParams', 'dataSyncService', 'productDataService','productEditHelper',

                function ($scope, $location, $state, $stateParams, dataSyncService, productDataService, productEditHelper) {

                    var vm = this;
                    var id = $stateParams.id;
                    var loadtype = $location.url().replace('/ppts/product/copy/', '').replace(/\/.*$/, '');

                    productEditHelper.init($scope, vm, loadtype, productDataService, function () {

                        //加载信息
                        productDataService.copyProductView(id, function (entity) {

                            console.warn(entity);

                            vm.dictionaries = entity.dictionaries;

                            vm.m[loadtype].categoryType = entity.categoryType;
                            vm.m[loadtype].product = entity.product;
                            vm.m[loadtype].exOfCourse = entity.exOfCourse || vm.m[loadtype].exOfCourse;
                            vm.m[loadtype].salaryRules = entity.salaryRules || vm.m[loadtype].salaryRules;
                            vm.m[loadtype].catalogs = entity.catalogs;

                            dataSyncService.injectDictData(mcs.util.mapping(entity.catalogs, { key: 'catalog', value: 'catalogName' }, 'CategoryCustom'));
                            ppts.config.dictMappingConfig["categoryCustom"] = "c_codE_ABBR_CategoryCustom";

                            $scope.$broadcast('dictionaryReady');


                            console.warn(vm.m[loadtype])
                            console.log(ppts.dict);

                        });

                    });

                    $scope.tag = '复制';

                }

            ]);
        });

