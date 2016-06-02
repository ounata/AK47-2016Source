define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.discountDataService],
        function (customer) {
            customer.registerController('discountListController', ['$scope', '$stateParams', 'utilService', 'discountDataService', 'discountDataViewService', 'discountListDataHeader', 'mcsDialogService',
                function ($scope, $stateParams, util, discountDataService, discountDataViewService, discountListDataHeader, mcsDialogService) {
                    var vm = this;

                    // 配置跟列表数据表头
                    discountDataViewService.configDiscountListHeaders(vm, discountListDataHeader);

                    vm.init = function () {
                        vm.criteria = vm.criteria || {};
                        discountDataViewService.initDiscountList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.init();

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
                                    message: '你确定要停用这条数据吗?'
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
                        mcs.util.postMockForm('http://localhost/PPTSWebApp/PPTS.WebAPI.Products/api/discount/exportAllDiscounts', vm.criteria);
                    };

                    vm.search = function () {
                        discountDataViewService.initDiscountList(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                }]);
        });