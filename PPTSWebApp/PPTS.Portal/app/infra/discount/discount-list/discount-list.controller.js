define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.discountDataService],
        function (customer) {
            customer.registerController('discountListController', ['$scope', '$stateParams', 'dataSyncService', 'utilService', 'discountDataService', 'discountListDataHeader', 'mcsDialogService',
                function ($scope, $stateParams, dataSyncService, util, discountDataService, discountListDataHeader, mcsDialogService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    dataSyncService.configDataHeader(vm, discountListDataHeader, discountDataService.getPagedDiscounts);

                    vm.search = function () {
                        vm.criteria = vm.criteria || {};
                        dataSyncService.injectDynamicDict('dateRange,ifElse');
                        dataSyncService.initDataList(vm, discountDataService.getAllDiscounts, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };

                    vm.search();

                    vm.delete = function () {
                        if (util.selectOneRow(vm)) {
                            vm.confirm = function () {
                                var dlg = mcsDialogService.confirm({
                                    title: '提示',
                                    message: '删除后折扣表不可见，你确定要删除这条数据吗?'
                                });
                                dlg.result.then(function () {
                                    vm.criteria = vm.criteria || {};
                                    vm.criteria.discountID = vm.data.rowsSelected[0].discountID;
                                    discountDataService.getDeleteDiscount(vm.criteria, function () {
                                        vm.init();
                                    });
                                })
                            };
                            vm.confirm();
                        }
                    };

                    vm.disable = function () {
                        if (util.selectOneRow(vm)) {
                            vm.confirm = function () {
                                var dlg = mcsDialogService.confirm({
                                    title: '提示',
                                    message: '该折扣表即将停用，请确认。'
                                });
                                dlg.result.then(function () {
                                    vm.criteria = vm.criteria || {};
                                    vm.criteria.discountID = vm.data.rowsSelected[0].discountID;
                                    discountDataService.getDisableDiscount(vm.criteria, function () {
                                        vm.init();
                                    });
                                })
                            };
                            vm.confirm();
                        }
                    };

                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.productApiBaseUrl + 'api/discount/exportAllDiscounts', vm.criteria);
                    };
                }]);
        });